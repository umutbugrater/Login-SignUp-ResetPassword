using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class ModalOrnekManager : IModalOrnekService
    {
        IModalOrnekDal _modalOrnekDal;

        public ModalOrnekManager(IModalOrnekDal modalOrnekDal)
        {
            _modalOrnekDal = modalOrnekDal;
        }

        public void TAdd(ModalOrnek t)
        {
            _modalOrnekDal.TAdd(t);
        }

        public void TDelete(ModalOrnek t)
        {
            _modalOrnekDal.TDelete(t);
        }

        public ModalOrnek TGetByID(int id)
        {
            return _modalOrnekDal.TGetByID(id);
        }

        public List<ModalOrnek> TGetList()
        {
            return _modalOrnekDal.TGetList();
        }

        public List<ModalOrnek> TGetList(Expression<Func<ModalOrnek, bool>> filter)
        {
           return _modalOrnekDal.TGetList(filter);
        }

        public void TUpdate(ModalOrnek t)
        {
            _modalOrnekDal.TUpdate(t);
        }
    }
}
