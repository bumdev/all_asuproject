using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Абонент — юридическое лицо
/// </summary>
/// 
namespace Entities
{
    public class UAbonent : UniversalEntity
    {
        #region Attributes

        int _ID;
        string _Title;       
        int _DogID;
        string _NumberJournal;
        string _OKPO;        
        string _MFO;        
        string _RS;        
        string _Contract;        
        string _Bank;        
        string _Address;        
        bool _IsBudget;
        string _Phone;
        string _ContactFace;
        string _Cause;
        string _INN;
        string _VATPay;
        string _TaxType;

        

        #endregion

        #region Properties

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public string NumberJournal
        {
            get { return _NumberJournal; }
            set { _NumberJournal = value; }
        }
        /// <summary>
        /// Название организации
        /// </summary>
        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }
        /// <summary>
        /// ID договора
        /// </summary>
        public int DogID
        {
            get { return _DogID; }
            set { _DogID = value; }
        }
        /// <summary>
        /// ОКПО
        /// </summary>
        public string OKPO
        {
            get { return _OKPO; }
            set { _OKPO = value; }
        }
        /// <summary>
        /// МФО
        /// </summary>
        public string MFO
        {
            get { return _MFO; }
            set { _MFO = value; }
        }
        /// <summary>
        /// Расчетный счет
        /// </summary>
        public string RS
        {
            get { return _RS; }
            set { _RS = value; }
        }
        /// <summary>
        /// Договор
        /// </summary>
        public string Contract
        {
            get { return _Contract; }
            set { _Contract = value; }
        }
        /// <summary>
        /// Отделение банка вс которым работает организация
        /// </summary>
        public string Bank
        {
            get { return _Bank; }
            set { _Bank = value; }
        }
        /// <summary>
        /// Юридический адрес
        /// </summary>
        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }
        /// <summary>
        /// Признак того, что организация бюджетная 
        /// </summary>
        public bool IsBudget
        {
            get { return _IsBudget; }
            set { _IsBudget = value; }
        }

        /// <summary>
        /// Контактный телефон
        /// </summary>
        public string Phone
        {
            get { return _Phone; }
            set { _Phone = value; }
        }
        /// <summary>
        /// ДКонтактное лицо
        /// </summary>
        public string ContactFace
        {
            get { return _ContactFace; }
            set { _ContactFace = value; }
        }
        /// <summary>
        /// Основание для договора
        /// </summary>
        public string Cause
        {
            get { return _Cause; }
            set { _Cause = value; }
        }
        /// <summary>
        /// Индивидуальный номер налогоплательщика ИНН
        /// </summary>
        public string INN
        {
            get { return _INN; }
            set { _INN = value; }
        }
        /// <summary>
        /// Свидетельство плательщика НДС
        /// </summary>
        public string VATPay
        {
            get { return _VATPay; }
            set { _VATPay = value; }
        }
        /// <summary>
        /// Тип налогообложения
        /// </summary>
        public string TaxType
        {
            get { return _TaxType; }
            set { _TaxType = value; }
        }
        #endregion

        #region Methods
        public UAbonent()
        {
            _ID = 0;
            _NumberJournal = "";
            _Title = "";
            _DogID = 0;
            _OKPO = "";
            _MFO = "";
            _RS = "";
            _Contract = "";
            _Bank = "";
            _Address = "";
            _IsBudget = false;
            _Phone = "";
            _ContactFace = "";
            _Cause = "";
            _INN = "";
            _VATPay = "";
            _TaxType = "";
        }
        #endregion
    }
}