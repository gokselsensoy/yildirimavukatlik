using Business.Abstract;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ContactManager : IContactService
    {
        private readonly IContactDal _contactDal;
        public ContactManager(IContactDal contactDal)
        {
            _contactDal = contactDal;
        }

        [CacheRemoveAspect("ContactService.Get")]
        public IDataResult<int> Add(Contact contact)
        {
            _contactDal.Add(contact);
            var result = _contactDal.Get(c =>
            c.Name == contact.Name &&
            c.Email == contact.Email &&
            c.PhoneNumber == contact.PhoneNumber &&
            c.Message == contact.Message);
            if (result != null)
            {
                return new SuccessDataResult<int>(result.Id);
            }
            return new ErrorDataResult<int>(-1, "Mesajınız Gönderilemedi");
        }

        [CacheRemoveAspect("ContactService.Get")]
        public IResult Delete(Contact contact)
        {
            var result = BusinessRules.Run(CheckIfContactIdExist(contact.Id));
            if (result != null)
            {
                return result;
            }

            var deletedContact = _contactDal.Get(c => c.Id == contact.Id);;
            _contactDal.Delete(deletedContact);
            return new SuccessResult(Messages.MessageDeleted);
        }

        [CacheAspect]
        public IDataResult<List<Contact>> GetAll()
        {
            return new SuccessDataResult<List<Contact>>(_contactDal.GetAll());
        }

        private IResult CheckIfContactIdExist(int contactId)
        {
            var result = _contactDal.GetAll(c => c.Id == contactId).Any();
            if (!result)
            {
                return new ErrorResult(Messages.MessageNotExist);
            }
            return new SuccessResult();
        }

    }
}
