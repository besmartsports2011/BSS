using System;
using System.Collections.Generic;
using System.Linq;
using GM_WebAPI.DataAccessRepository;
using GM_WebAPI.Models;
using GM_WebAPI.Models.Entities;
using GM_WebAPI.Utils;

namespace GM_WebAPI.BusinessLayer
{
    public class CompanyManager
    {
        IDataAccessRepository<COMPANY, int> _companyRepository;
        private IDataAccessRepository<USER, int> _userRepository;
        public CompanyManager(IDataAccessRepository<COMPANY, int> companyRepository = null,
            IDataAccessRepository<USER, int> userRepository = null)
        {
            _companyRepository = companyRepository;
            _userRepository = userRepository;
        }

        private GMEntities _context { get; } = new GMEntities();

        public Company StartNewCompanyFlow(Company company, string baseUrl)
        {
            //Email existance
            if (UserEmailExists(company.Email))
            {
#if !DEBUG
                throw new Exception("User email already exists. Please use different contact email address.");
#endif
            }

            USER newUser = null;
            COMPANY newCompany = null;
            // transaction started...
            using (var context = new GMEntities())
            {
             
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        //Contact person user creation 
                        newUser = CreateCompanyUser(context, company);

                        //Company creation
                        newCompany = CreateCompany(context, company, newUser.ID);

                        company.Id = newCompany.ID;
                        company.ContactName = newCompany.CONTACT_NAME;

                        dbContextTransaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                        throw new Exception("Error occured while creating new company. Transaction rolled-back.");
                    }
                }
            }

            try
            {
                //Eamil to user
                bool emailSentToUser = SendUserEmail(newCompany, newUser, baseUrl, new Email {To = company.Email});

                //Eamil to user
                bool emailSentToAdmin = SendAdminEmail(newCompany);


                if (!emailSentToUser || !emailSentToAdmin)
                {
                    throw new Exception("Problem in sending emails");
                }

            }
            catch (Exception)
            {

            }
            return company;
        }

       
#region Private Methods
        private bool UserEmailExists(string email)
        {
            //return false;
            return (null != _context.USERs.FirstOrDefault(user => user.EMAIL.ToLower() == email.ToLower()));
        }

        private USER CreateCompanyUser(GMEntities context, Company company)
        {
            var gmUser = new USER
            {
                FIRST_NAME = company.ContactName,
                EMAIL = company.Email,
#if DEBUG
                USERNAME = company.Email + "-" + RandomToken.GenerateDigitsOnly(5),
#else

                USERNAME = company.Email,
#endif
                PASSWORD = RandomToken.GenerateAlphaNumeric(8),
                CR_BY = company.CrBy,
                CR_AT = company.CrAt,
                IS_ACTIVE = true,
                IS_EMAIL_VERIFIED = false,
                GUID = Guid.NewGuid(),
                TOKEN = RandomToken.GenerateLowerAlphaNumeric(16)
            };
            context.USERs.Add(gmUser);
            context.SaveChanges();
            return gmUser;
        }
        private COMPANY CreateCompany(GMEntities context, Company company, int newUserId)
        {
            var gmCompany = new COMPANY
            {
                NAME = company.Name,
                ADDRESS_LINE_1 = company.AddressLine1,
                ADDRESS_LINE_2 = company.AddressLine2,
                SUBURB = company.Subarb,
                POST_CODE = company.PostCode,
                COUNTRY = company.Country,
                PHONE = company.Phone,
                ABN = company.Abn,
                ACCOUNT_NO = RandomToken.GenerateDigitsOnly(11),
                BSB = GlobalSettings.Instance.Bsb,
                IS_ACTIVE = company.IsActive,
                IS_ADMIN = company.IsAdmin,
                IS_DELETED = company.IsDeleted,
                CR_BY = company.CrBy,
                CR_AT = company.CrAt,
                UP_BY = company.UpBy,
                UP_AT = company.UpAt,
                LOGO_PATH = company.LogoPath,
                AGREEMENT_DATE = company.AgreementDate,
                AGREEMENT_PATH = company.AgreementPath,
                EMAIL = company.Email,
                CONTACT_USER_ID = newUserId,
                GUID = Guid.NewGuid(),
                ADMIN_FEE = company.AdminFee

            };
            context.COMPANies.Add(gmCompany);
            context.SaveChanges();
            return gmCompany;
        }

        public COMPANY GetAdminCompany()
        {
            return _context.COMPANies.First(x => x.IS_ADMIN == true); 
        }

        private bool SendUserEmail(COMPANY company, USER user, string baseUrl, Email email)
        {
            var body = GlobalSettings.Instance.UserEmailBody;

            body = body.Replace("{{name}}", company.NAME + ",");
            body = body.Replace("{{username}}", user.USERNAME);
            body = body.Replace("{{password}}", user.PASSWORD);
            body = body.Replace("{{accountno}}", company.ACCOUNT_NO);
            body = body.Replace("{{bsb}}", company.BSB);
            var activationLink = $"{baseUrl}/api/v1/signup/verify-email/tokens/{user.TOKEN}/users/{user.GUID.ToString().ToLower()}";
            body = body.Replace("{{link}}", "Click <a href='" + activationLink + "'>Here</a> to Activate ");
            email.Body = body; 
            email.Subject = GlobalSettings.Instance.UserEmailSubject;

            return EmailNotificationUtil.Send(email);
        }

        private bool SendAdminEmail(COMPANY company)
        {
            Email email = new Email();
            email.To = GetAdminCompany().EMAIL;

            var body = GlobalSettings.Instance.AdminEmailBody;

            body = body.Replace("{{companyname}}", company.NAME);
            body = body.Replace("{{username}}", company.CONTACT_NAME);
            body = body.Replace("{{accountno}}", company.ACCOUNT_NO);
            body = body.Replace("{{bsb}}", company.BSB);
            email.Body = body;
            email.Subject = GlobalSettings.Instance.UserEmailSubject;

            return EmailNotificationUtil.Send(email);
        }

#endregion

        public IEnumerable<CompanySearchView> SearchCompany(short size, bool archived, string criteria)
        {
            if (string.IsNullOrEmpty(criteria))
            {
                return new CompanySearchView[0];
            }
            return _context.SearchCompanies(criteria, size, archived)
                .Select(x => new CompanySearchView
                {
                   Id = x.ID,
                   Name = x.NAME,
                   AddressLine1 = x.ADDRESS_LINE_1,
                   AddressLine2 = x.ADDRESS_LINE_2,
                   Abn = x.ABN,
                   IsActive = x.IS_ACTIVE,
                   CrAt = x.CR_AT,
                   Email = x.EMAIL,
                   ContactName = x.CONTACT_NAME,
                   Guid = x.GUID,
                   AdminFee = x.ADMIN_FEE,
                   AccountNo = x.ACCOUNT_NO,
                   Bsb = x.BSB,
                   IsDeleted = x.IS_DELETED
                }).ToList();
        }

        internal void UndeleteCompanies(int[] ids)
        {
            foreach (var id in ids)
            {
                var book = _context.COMPANies.Find(id);
                if (book != null)
                {
                    book.IS_DELETED = false;
                    _context.SaveChanges();
                }

            }
        }

        public object GetCompanyProfile(string userGuid)
        {
            if (string.IsNullOrEmpty(userGuid))
            {
                return null;
            }

            var db_user = _context.USERs.FirstOrDefault(x => x.GUID == new Guid(userGuid));
            if (db_user != null)
            {
                var user = new User
                {
                    Id = db_user.ID,
                    FirstName = db_user.FIRST_NAME,
                    MiddleName = db_user.MIDDLE_NAME,
                    LastName = db_user.LAST_NAME,
                    Email = db_user.EMAIL,
                    UserName = db_user.USERNAME,
                    Password = "",
                    AddressLine1 = db_user.ADDRESS_LINE_1,
                    AddressLine2 = db_user.ADDRESS_LINE_2,
                    Suburb = db_user.SUBURB,
                    PostCode = db_user.POST_CODE,
                    Country = db_user.COUNTRY,
                    Phone = db_user.PHONE,
                    Mobile = db_user.MOBILE,
                    CrBy = db_user.CR_BY,
                    CrAt = db_user.CR_AT,
                    UpBy = db_user.UP_BY,
                    UpAt = db_user.UP_AT,
                    PhotoPath = db_user.PHOTO_PATH,
                    IsEmailVerified = db_user.IS_EMAIL_VERIFIED,
                    IsActive = db_user.IS_ACTIVE,
                    Guid = db_user.GUID,
                    Token = ""
                };

                var db_company = _context.COMPANies.FirstOrDefault(x => x.CONTACT_USER_ID == user.Id);
                if (db_company != null)
                {
                    var company = new Company()
                    {
                        Id = db_company.ID,
                        Name = db_company.NAME,
                        AddressLine1 = db_company.ADDRESS_LINE_1,
                        AddressLine2 = db_company.ADDRESS_LINE_2,
                        Subarb = db_company.SUBURB,
                        PostCode = db_company.POST_CODE,
                        Country = db_company.COUNTRY,
                        Phone = db_company.PHONE,
                        Abn = db_company.ABN,
                        IsAdmin = db_company.IS_ADMIN,
                        IsActive = db_company.IS_ACTIVE,
                        IsDeleted = db_company.IS_DELETED,
                        CrBy = db_company.CR_BY,
                        CrAt = db_company.CR_AT,
                        UpBy = db_company.UP_BY,
                        UpAt = db_company.UP_AT,
                        LogoPath = db_company.LOGO_PATH,
                        AgreementPath = db_company.AGREEMENT_PATH,
                        AgreementDate = db_company.AGREEMENT_DATE,
                        Email = db_company.EMAIL,
                        ContactUserId = db_company.CONTACT_USER_ID,
                        ContactName = db_company.CONTACT_NAME,
                        Guid = db_company.GUID,
                        AdminFee = db_company.ADMIN_FEE,
                    };

                    return new
                    {
                        company = company,
                        user = user
                    };
                }
            }
            return null;
        }

    }
}