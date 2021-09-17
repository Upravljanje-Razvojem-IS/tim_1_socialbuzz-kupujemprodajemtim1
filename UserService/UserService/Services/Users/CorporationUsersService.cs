using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Data;
using UserService.Entities;
using UserService.Exceptions;

namespace UserService.Services.Users
{
    public class CorporationUsersService : ICorporationUsersService
    {

        private readonly IMapper _mapper;
        private readonly IRoleRepository _roleRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IPersonalUserRepository _personalUserRepository;
        private readonly UserManager<AccountInfo> _userManager;
        private readonly ICorporationUserRepository _corporationUserRepository;
        public CorporationUsersService(ICorporationUserRepository corporationUserRepository, IMapper mapper,
            IRoleRepository roleRepository, ICountryRepository countryRepository,
            IPersonalUserRepository personalUserRepository, UserManager<AccountInfo> userManager)
        {
            _corporationUserRepository = corporationUserRepository;
            _personalUserRepository = personalUserRepository;
            _roleRepository = roleRepository;
            _userManager = userManager;
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        public CorporationUserCreatingConfirmation CreateUser(CorporationUser corpUser, string password)
        {
            if (!CheckUniqueUsername(corpUser.Username, false, null))
            {
                throw new UniqueValueViolationException("Username should be unique");
            }
            if (!CkeckUniqueEmail(corpUser.Email))
            {
                throw new UniqueValueViolationException("Email should be unique");
            }
            if (!CheckCountry(corpUser.CountryId))
            {
                throw new ForeignKeyConstraintViolationException("Foreign key constraint violated");

            }
            CorporationUserCreatingConfirmation userCreated = _corporationUserRepository.CreateUser(corpUser);
            _corporationUserRepository.SaveChanges();


            //Dodavanje u IdentityUserDbContext
            string username = string.Join("", corpUser.Username.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
            var acc = new AccountInfo(username, corpUser.Email, userCreated.UserId);
            IdentityResult result = _userManager.CreateAsync(acc, password).Result;
            if (result.Succeeded)
            {
                _userManager.AddToRoleAsync(acc, "Regular user").Wait();
            }
            else
            {
                _corporationUserRepository.DeleteUser(userCreated.UserId);

            }
            return userCreated;
        }

        public void DeleteUser(Guid userId)
        {
            _corporationUserRepository.DeleteUser(userId);
            _corporationUserRepository.SaveChanges();

            //Brisanje iz identity tabele
            var corpUser = _userManager.FindByIdAsync(userId.ToString()).Result;
            _userManager.DeleteAsync(corpUser).Wait();
        }

        public CorporationUser GetUserByUserId(Guid userId)
        {
            return _corporationUserRepository.GetUserByUserId(userId);
        }

        public List<CorporationUser> GetUsers(string country = null, string username = null)
        {
            return _corporationUserRepository.GetUsers(country, username);
        }

        public void UpdateUser(CorporationUser updatedCorpUser, CorporationUser userWithId)
        {
            if (!CheckUniqueUsername(updatedCorpUser.Username, true, userWithId.UserId))
            {
                throw new UniqueValueViolationException("Username should be unique");
            }
            Country country = _countryRepository.GetCountryByCountryId(updatedCorpUser.CountryId);
            if (country == null)
            {
                throw new ForeignKeyConstraintViolationException("Foreign key constraint violated");

            }
            updatedCorpUser.RoleId = userWithId.RoleId;
            updatedCorpUser.Email = userWithId.Email;
            updatedCorpUser.Role = _roleRepository.GetRoleByRoleId(userWithId.RoleId);
            updatedCorpUser.Country = country;
            updatedCorpUser.UserId = userWithId.UserId;
            _mapper.Map(updatedCorpUser, userWithId);
            _corporationUserRepository.SaveChanges();

            //Updajteovanje identity tabele
            AccountInfo corpUser = _userManager.FindByIdAsync(userWithId.UserId.ToString()).Result;
            string username = string.Join("", updatedCorpUser.Username.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
            corpUser.UserName = username;
            _userManager.UpdateAsync(corpUser).Wait();
        }

        private bool CheckUniqueUsername(string username, bool forUpdate, Guid? userId)
        {
            var corpUsers = _corporationUserRepository.GetUsers(null, username);
            if (corpUsers != null && corpUsers.Count > 0)
            {
                if (!forUpdate || (forUpdate && corpUsers[0].UserId != userId))
                {
                    return false;
                }
            }
            var persUsers = _personalUserRepository.GetUsers(null, username);
            if (persUsers != null && persUsers.Count > 0)
            {
                if (!forUpdate || (forUpdate && persUsers[0].UserId != userId))
                {
                    return false;
                }

            }
            return true;
        }

        private bool CkeckUniqueEmail(string email)
        {
            var users = _corporationUserRepository.GetUserWithEmail(email);
            if (users != null)
            {
                return false;
            }
            var persUsers = _personalUserRepository.GetUserWithEmail(email);
            if (persUsers != null)
            {
                return false;
            }
            return true;
        }

        private bool CheckCountry(Guid countryId)
        {
            return (_countryRepository.GetCountryByCountryId(countryId) != null);
        }

    }
}