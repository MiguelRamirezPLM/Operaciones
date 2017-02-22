using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLMClientsBusinessEntities;

namespace PLMClientsBusinessLogicComponent
{
  public  class BooksBLC
  {

      #region Constructor

       private BooksBLC() { }

      #endregion

      #region Public Methods

       public BookInfo getBookByCode(string code)
       {


           return PLMClientsDataAccessComponent.BooksDALC.Instance.getBookByCode(code);
           

       
       }

      #endregion

       public static readonly BooksBLC Instance = new BooksBLC();
  }
}
