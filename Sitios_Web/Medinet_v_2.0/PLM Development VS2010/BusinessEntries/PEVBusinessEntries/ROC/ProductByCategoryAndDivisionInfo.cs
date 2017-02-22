using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;


namespace PEVBusinessEntries.ROC
{
    [DataContract]
   public class ProductByCategoryAndDivisionInfo
   {
       #region Constructor

    public ProductByCategoryAndDivisionInfo()  { }

       #endregion

    #region Poperties

    [DataMember]
    public int ProductId
    {
        get { return this._productId; }
        set { this._productId = value; }
    }

    [DataMember]
    public string ProductName
    {
        get { return this._productname; }
        set { this._productname = value; }
    }

    [DataMember]
    public byte CountryId
    {
        get { return this._countryId; }
        set { this._countryId = value; }
    }

    [DataMember]
    public int LaboratoryId
    {
        get { return this._laboratoryId; }
        set { this._laboratoryId = value; }
    }

    [DataMember]
    public string Description
    {
        get { return this._description; }
        set { this._description = value; }
    }

    [DataMember]
    public bool Active
    {
        get { return this._active; }
        set { this._active = value; }
    }

    [DataMember]
    public string Participant
    {
        get { return this._participant; }
        set { this._participant = value; }
    }

    [DataMember]
    public int CategoryId
    {
        get { return this._categoryId; }
        set { this._categoryId = value; }
    }
    #endregion 

    private int _productId;
    private string _productname;
    private byte _countryId;
    private int _laboratoryId;
    private string _description;
    private bool _active;
    private string _participant;
    private int _categoryId;



   }



   

}
