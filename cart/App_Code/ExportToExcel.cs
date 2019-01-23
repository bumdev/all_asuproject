using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.IO;
using DAO;
using DomainObjects;
using Entities;
using cartridges_app.Controls;
using NPOI.HSSF.UserModel;
using NPOI.HPSF;
using NPOI.POIFS.FileSystem;
using NPOI.SS.UserModel;
using Entities;
using DomainObjects;

namespace cartridges_app
{
    public class ExportToExcel
    {
        //Получение номера буквы для адресации в Excel
        public static int GetLetterNumber(string letter)
        {
            int num = 0;
            string abc = "abcdefghijklmnopqrstuvwxyz";

            for (int j = 0; j < letter.Length; j++)
            {
                if (j == letter.Length - 1)
                {
                    for (int i = 0; i < abc.Length; i++)
                    {
                        if (letter[j] == abc[i])
                        {
                            num += i;
                        }
                    }
                }
                else
                {
                    num += 26;
                }
            }
            return num;
        }
        public static void GenerateUAct(int id)
        {
            UniversalEntity ue = new UniversalEntity();
            UAbonent ua = new UAbonent();
            UAbonentDO uado = new UAbonentDO();
            UOrder uo = new UOrder();
            UOrderDO uodo = new UOrderDO();
            UOrderDetailsDO uodDO = new UOrderDetailsDO();

            ue = uado.RetrieveByOrderID(id);
            string header = string.Empty;
            string actNumber = string.Empty;
            if (ue.Count > 0)
            {
                ua = (UAbonent)ue[0];
                header += ua.Title + " \nюр адрес: " + ua.Address + " \n№ св. пл. НДС: " + ua.VATPay + " \n№ ИНН: " + ua.INN + " \nтел.: " + ua.Phone;
            }
            ue = uodo.RetrieveUOrderById(id);
            if (ue.Count > 0)
            {
                uo = (UOrder)ue[0];
                if (uo.DateOut.HasValue)
                {
                    actNumber = "  г. Донецк            АКТ № " + uo.ID + " за _________________________________г.";
                }
                else
                {
                    actNumber = "  г. Донецк            АКТ № " + uo.ID + " за _________________________________ г.";
                }
            }

            ue = uodDO.RetrieveUOrderDetailsByOrderID(id);
           // " от " + RuDateAndMoneyConverter.DateToTextLong(fo.DateIn) + " г.";
            InitializeWorkbook();

            ISheet sheet1 = hssfworkbook.GetSheet("s");

            sheet1.GetRow(11).GetCell(0).SetCellValue("                             по договору (письму) № " + uo.ID + " от " + RuDateAndMoneyConverter.DateToTextLong(uo.DateIn) + " г.");

            sheet1.GetRow(41).GetCell(0).SetCellValue("                             по договору (письму) № " + uo.ID +  " от " + RuDateAndMoneyConverter.DateToTextLong(uo.DateIn) + " г.");

            //create cell on rows, since rows do already exist,it's not necessary to create rows again.
            sheet1.GetRow(2).GetCell(3).SetCellValue(header);
            sheet1.GetRow(32).GetCell(3).SetCellValue(header);
            sheet1.GetRow(9).GetCell(0).SetCellValue(actNumber);
            sheet1.GetRow(39).GetCell(0).SetCellValue(actNumber);

            int rowC = 44;
            int row = 14;
            double sum = 0;

            int count = ue.Count;

            if (count <= 5)
            {
                CustomRetrieverDO crDO = new CustomRetrieverDO();
                ue = crDO.RetrieveUActByOrderID5Low(id);
                if (ue.Count > 0)
                {
                    for (int i = row; i < ue.Count + row; i++)
                    {
                        ArrayList al = (ArrayList)ue[i - row];
                        //FOrderDetailsAct u = (FOrderDetailsAct)fodal[i - row];

                        sheet1.GetRow(i).GetCell(0).SetCellValue(i - row + 1);
                        sheet1.GetRow(i).GetCell(1).SetCellValue(string.Format("Определения метрологических характеристик водомера D{0}", al[0].ToString()));
                        sheet1.GetRow(i).GetCell(2).SetCellValue("калькуляция");
                        sheet1.GetRow(i).GetCell(3).SetCellValue("шт.");
                        sheet1.GetRow(i).GetCell(4).SetCellValue(al[4].ToString());
                        sheet1.GetRow(i).GetCell(5).SetCellValue(Convert.ToDouble(al[3]).ToString("0.00"));
                        sheet1.GetRow(i).GetCell(6).SetCellValue(Convert.ToDouble(al[3]).ToString("0.00"));


                        sheet1.GetRow(rowC).GetCell(0).SetCellValue(i - row + 1);
                        sheet1.GetRow(rowC).GetCell(1).SetCellValue(string.Format("Определения метрологических характеристик водомера  D{0}", al[0].ToString()));
                        sheet1.GetRow(rowC).GetCell(2).SetCellValue("калькуляция");
                        sheet1.GetRow(rowC).GetCell(3).SetCellValue("шт.");
                        sheet1.GetRow(rowC).GetCell(4).SetCellValue(al[4].ToString());
                        sheet1.GetRow(rowC).GetCell(5).SetCellValue(Convert.ToDouble(al[3]).ToString("0.00"));
                        sheet1.GetRow(rowC).GetCell(6).SetCellValue(Convert.ToDouble(al[3]).ToString("0.00"));

                        sum += Convert.ToDouble(al[3]);
                        rowC++;
                    }
                }
            }
            else
            {
                CustomRetrieverDO crDO = new CustomRetrieverDO();
                ue = crDO.RetrieveUActByOrderID5High(id);
                if (ue.Count > 0)
                {
                    for (int i = row; i < ue.Count + row; i++)
                    {
                        ArrayList al = (ArrayList)ue[i - row];
                        //FOrderDetailsAct u = (FOrderDetailsAct)fodal[i - row];

                        sheet1.GetRow(i).GetCell(0).SetCellValue(i - row + 1);
                        sheet1.GetRow(i).GetCell(1).SetCellValue(string.Format("Определения метрологических характеристик водомера  D{0}", al[0].ToString()));
                        sheet1.GetRow(i).GetCell(2).SetCellValue("калькуляция");
                        sheet1.GetRow(i).GetCell(3).SetCellValue("шт.");
                        sheet1.GetRow(i).GetCell(4).SetCellValue(al[3].ToString());
                        sheet1.GetRow(i).GetCell(5).SetCellValue(Convert.ToDouble(al[1]).ToString("0.00"));
                        sheet1.GetRow(i).GetCell(6).SetCellValue(Convert.ToDouble(al[2]).ToString("0.00"));


                        sheet1.GetRow(rowC).GetCell(0).SetCellValue(i - row + 1);
                        sheet1.GetRow(rowC).GetCell(1).SetCellValue(string.Format("Определения метрологических характеристик водомера  D{0}", al[0].ToString()));
                        sheet1.GetRow(rowC).GetCell(2).SetCellValue("калькуляция");
                        sheet1.GetRow(rowC).GetCell(3).SetCellValue("шт.");
                        sheet1.GetRow(rowC).GetCell(4).SetCellValue(al[3].ToString());
                        sheet1.GetRow(rowC).GetCell(5).SetCellValue(Convert.ToDouble(al[1]).ToString("0.00"));
                        sheet1.GetRow(rowC).GetCell(6).SetCellValue(Convert.ToDouble(al[2]).ToString("0.00"));

                        sum += Convert.ToDouble(al[2]);
                        rowC++;
                    }
                }
            }






            /*int rowC = 44;
            int row = 14;
            double sum = 0;
            if (ue.Count > 0)
            {
                for (int i = row; i < ue.Count + row; i++)
                {
                    UOrderDetails u = (UOrderDetails)ue[i - row];

                    sheet1.GetRow(i).GetCell(0).SetCellValue(i - row+1);
                    sheet1.GetRow(i).GetCell(1).SetCellValue(string.Format("Определения метрологических характеристик водомера  D{0}, {1}, {2}", al[0].ToString(), al[1].ToString(), al[2].ToString()));
                    sheet1.GetRow(i).GetCell(2).SetCellValue("калькуляция");
                    sheet1.GetRow(i).GetCell(3).SetCellValue("шт.");
                    sheet1.GetRow(i).GetCell(4).SetCellValue("1");
                    sheet1.GetRow(i).GetCell(5).SetCellValue(u.Price.ToString("0.00"));
                    sheet1.GetRow(i).GetCell(6).SetCellValue(u.Price.ToString("0.00"));

                    sheet1.GetRow(rowC).GetCell(0).SetCellValue(i - row + 1);
                    sheet1.GetRow(rowC).GetCell(1).SetCellValue(string.Format("Определения метрологических характеристик водомера  D{0}, {1}, {2}", al[0].ToString(), al[1].ToString(), al[2].ToString()));
                    sheet1.GetRow(rowC).GetCell(2).SetCellValue("калькуляция");
                    sheet1.GetRow(rowC).GetCell(3).SetCellValue("шт.");
                    sheet1.GetRow(rowC).GetCell(4).SetCellValue("1");
                    sheet1.GetRow(rowC).GetCell(5).SetCellValue(u.Price.ToString("0.00"));
                    sheet1.GetRow(rowC).GetCell(6).SetCellValue(u.Price.ToString("0.00"));

                    sum += u.Price;
                    rowC++;
                }
            }*/

            sheet1.GetRow(19).GetCell(6).SetCellValue(sum.ToString("0.00"));
            sheet1.GetRow(20).GetCell(6).SetCellValue(Utilities.GetVAT(sum).ToString("0.00"));
            sheet1.GetRow(21).GetCell(6).SetCellValue((sum + Utilities.GetVAT(sum)).ToString("0.00"));

            sheet1.GetRow(49).GetCell(6).SetCellValue(sum.ToString("0.00"));
            sheet1.GetRow(50).GetCell(6).SetCellValue(Utilities.GetVAT(sum).ToString("0.00"));
            sheet1.GetRow(51).GetCell(6).SetCellValue((sum + Utilities.GetVAT(sum)).ToString("0.00"));
            /* sheet1.GetRow(2).GetCell(1).SetCellValue(300);
             sheet1.GetRow(3).GetCell(1).SetCellValue(500050);
             sheet1.GetRow(4).GetCell(1).SetCellValue(8000);
             sheet1.GetRow(5).GetCell(1).SetCellValue(110);
             sheet1.GetRow(6).GetCell(1).SetCellValue(100);
             sheet1.GetRow(7).GetCell(1).SetCellValue(200);
             sheet1.GetRow(8).GetCell(1).SetCellValue(210);
             sheet1.GetRow(9).GetCell(1).SetCellValue(2300);
             sheet1.GetRow(10).GetCell(1).SetCellValue(240);
             sheet1.GetRow(11).GetCell(1).SetCellValue(180123);
             sheet1.GetRow(12).GetCell(1).SetCellValue(150);*/

            //Force excel to recalculate all the formula while open
            sheet1.ForceFormulaRecalculation = true;

            WriteToFile();
        }
        public static void GenerateFAct(int id)
        {
            UniversalEntity ue = new UniversalEntity();
            string header = string.Empty;
            string actNumber = string.Empty;
            string actNumber2 = string.Empty;
            string footer = string.Empty;

            FAbonentDO faDO = new FAbonentDO();

            ue = faDO.RetrieveByOrderID(id);
            if (ue.Count > 0)
            {
                FAbonent fa = (FAbonent)ue[0];
                header += fa.Surname + " " + fa.FirstName + " " + fa.LastName + " \nадрес: " + fa.Address + "\nтел.: " + fa.Phone;
            }

            FOrder fo = new FOrder();
            FOrderDO foDO = new FOrderDO();
            FOrderDetailsDO fodDO = new FOrderDetailsDO();

            ue = foDO.RetrieveFOrderById(id);
            if (ue.Count > 0)
            {
                fo = (FOrder)ue[0];
                actNumber = "  г. Донецк            АКТ № " + fo.Prefix + fo.ID + " за _________________________________г.";
                actNumber2 = "                             по договору (письму) №  " + fo.Prefix + fo.ID + " от " + RuDateAndMoneyConverter.DateToTextLong(fo.DateIn) + " г.";
            }

            ue = faDO.RetrieveByOrderID(id);
            if (ue.Count > 0)
            {
                FAbonent fa = (FAbonent) ue[0];
                footer += " Исполнитель:__________________Ломанов В.И. " + "                                           Заказчик:_______________" + fa.Surname + " " + fa.FirstName + " " + fa.LastName;
            }

            ue = fodDO.RetrieveFOrderDetailsByOrderID(id);

            InitializeWorkbookFAct(); //InitializeWorkbook();
            ISheet sheet1 = hssfworkbook.GetSheet("s");
            //create cell on rows, since rows do already exist,it's not necessary to create rows again.
            sheet1.GetRow(2).GetCell(3).SetCellValue(header);
            sheet1.GetRow(32).GetCell(3).SetCellValue(header);
            sheet1.GetRow(9).GetCell(0).SetCellValue(actNumber);
            sheet1.GetRow(39).GetCell(0).SetCellValue(actNumber);
            sheet1.GetRow(11).GetCell(0).SetCellValue(actNumber2);
            sheet1.GetRow(41).GetCell(0).SetCellValue(actNumber2);
            sheet1.GetRow(29).GetCell(0).SetCellValue(footer);
            sheet1.GetRow(59).GetCell(0).SetCellValue(footer);

            int rowC = 44;
            int row = 14;
            double sum = 0;

            int count = ue.Count;

            if (count <= 5)
            {
                CustomRetrieverDO crDO = new CustomRetrieverDO();
                ue = crDO.RetrieveFActByOrderID5Low(id);
                if (ue.Count > 0)
                {
                    for (int i = row; i < ue.Count + row; i++)
                    {
                        ArrayList al = (ArrayList)ue[i - row];
                        //FOrderDetailsAct u = (FOrderDetailsAct)fodal[i - row];

                        sheet1.GetRow(i).GetCell(0).SetCellValue(i - row + 1);
                        sheet1.GetRow(i).GetCell(1).SetCellValue(string.Format("Определения метрологических характеристик водомера  D{0}", al[0].ToString()));
                        sheet1.GetRow(i).GetCell(2).SetCellValue("калькуляция");
                        sheet1.GetRow(i).GetCell(3).SetCellValue("шт.");
                        sheet1.GetRow(i).GetCell(4).SetCellValue(al[4].ToString());
                        sheet1.GetRow(i).GetCell(5).SetCellValue(Convert.ToDouble(al[3]).ToString("0.00"));
                        sheet1.GetRow(i).GetCell(6).SetCellValue(Convert.ToDouble(al[3]).ToString("0.00"));


                        sheet1.GetRow(rowC).GetCell(0).SetCellValue(i - row + 1);
                        sheet1.GetRow(rowC).GetCell(1).SetCellValue(string.Format("Определения метрологических характеристик водомера  D{0}", al[0].ToString()));
                        sheet1.GetRow(rowC).GetCell(2).SetCellValue("калькуляция");
                        sheet1.GetRow(rowC).GetCell(3).SetCellValue("шт.");
                        sheet1.GetRow(rowC).GetCell(4).SetCellValue(al[4].ToString());
                        sheet1.GetRow(rowC).GetCell(5).SetCellValue(Convert.ToDouble(al[3]).ToString("0.00"));
                        sheet1.GetRow(rowC).GetCell(6).SetCellValue(Convert.ToDouble(al[3]).ToString("0.00"));

                        sum += Convert.ToDouble(al[3]);
                        rowC++;
                    }
                }
            }
            else
            {
                CustomRetrieverDO crDO = new CustomRetrieverDO();
                ue = crDO.RetrieveFActByOrderID5High(id);
                if (ue.Count > 0)
                {
                    for (int i = row; i < ue.Count + row; i++)
                    {
                        ArrayList al = (ArrayList)ue[i - row];
                        //FOrderDetailsAct u = (FOrderDetailsAct)fodal[i - row];

                        sheet1.GetRow(i).GetCell(0).SetCellValue(i - row + 1);
                        sheet1.GetRow(i).GetCell(1).SetCellValue(string.Format("Определения метрологических характеристик водомера  D{0}", al[0].ToString()));
                        sheet1.GetRow(i).GetCell(2).SetCellValue("калькуляция");
                        sheet1.GetRow(i).GetCell(3).SetCellValue("шт.");
                        sheet1.GetRow(i).GetCell(4).SetCellValue(al[3].ToString());
                        sheet1.GetRow(i).GetCell(5).SetCellValue(Convert.ToDouble(al[1]).ToString("0.00"));
                        sheet1.GetRow(i).GetCell(6).SetCellValue(Convert.ToDouble(al[2]).ToString("0.00"));


                        sheet1.GetRow(rowC).GetCell(0).SetCellValue(i - row + 1);
                        sheet1.GetRow(rowC).GetCell(1).SetCellValue(string.Format("Определения метрологических характеристик водомера  D{0}", al[0].ToString()));
                        sheet1.GetRow(rowC).GetCell(2).SetCellValue("калькуляция");
                        sheet1.GetRow(rowC).GetCell(3).SetCellValue("шт.");
                        sheet1.GetRow(rowC).GetCell(4).SetCellValue(al[3].ToString());
                        sheet1.GetRow(rowC).GetCell(5).SetCellValue(Convert.ToDouble(al[1]).ToString("0.00"));
                        sheet1.GetRow(rowC).GetCell(6).SetCellValue(Convert.ToDouble(al[2]).ToString("0.00"));

                        sum += Convert.ToDouble(al[2]);
                        rowC++;
                    }
                }
            }



            sheet1.GetRow(19).GetCell(6).SetCellValue(sum.ToString("0.00"));
            sheet1.GetRow(20).GetCell(6).SetCellValue((sum / 100 * 20).ToString("0.00"));
            sheet1.GetRow(21).GetCell(6).SetCellValue((sum + (sum / 100 * 20)).ToString("0.00"));

            sheet1.GetRow(49).GetCell(6).SetCellValue(sum.ToString("0.00"));
            sheet1.GetRow(50).GetCell(6).SetCellValue((sum / 100 * 20).ToString("0.00"));
            sheet1.GetRow(51).GetCell(6).SetCellValue((sum + (sum / 100 * 20)).ToString("0.00"));

            //sheet1.GetRow(35).GetCell(GetLetterNumber("a")).SetCellValue("Загальна сума, що підлягає оплаті: " + UADateAndMoneyConverter.CurrencyToTxt((sum + Utilities.GetVAT(sum)), true));
            sheet1.GetRow(24).GetCell(GetLetterNumber("a")).SetCellValue("Сумма прописью: " + RuDateAndMoneyConverter.CurrencyToTxt((sum + Utilities.GetVAT(sum)), true));
            sheet1.GetRow(54).GetCell(GetLetterNumber("a")).SetCellValue("Сумма прописью: " + RuDateAndMoneyConverter.CurrencyToTxt((sum + Utilities.GetVAT(sum)), true));

            //Force excel to recalculate all the formula while open
            sheet1.ForceFormulaRecalculation = true;

            WriteToFileF();
        }
        public static void GenerateFActSpecial(int id, int ULocationID)
        {
            UniversalEntity ue = new UniversalEntity();
            string header = string.Empty;
            string actNumber = string.Empty;
            string actNumber2 = string.Empty;
            string footer = string.Empty;

            FAbonentDO faDO = new FAbonentDO();

            ue = faDO.RetrieveByOrderID(id);
            FAbonent fa = new FAbonent();
            if (ue.Count > 0)
            {
                fa = (FAbonent)ue[0];
                header += fa.Surname + " " + fa.FirstName + " " + fa.LastName + " \nадрес: " + fa.Address + "\nтел.: " + fa.Phone;
            }

            FOrder fo = new FOrder();
            FOrderDO foDO = new FOrderDO();
            FOrderDetailsDO fodDO = new FOrderDetailsDO();


            ue = foDO.RetrieveFOrderById(id);
            if (ue.Count > 0)
            {
                fo = (FOrder)ue[0];
                actNumber = "  г. Донецк            АКТ № " + fo.Prefix + fo.ID + " за _________________________________ г.";
                actNumber2 = "                             по договору (письму) №  " + fo.Prefix + fo.ID + " от " + RuDateAndMoneyConverter.DateToTextLong(fo.DateIn) + " г.";
            }

            ue = faDO.RetrieveByOrderID(id);
            if (ue.Count > 0)
            {
                fa = (FAbonent) ue[0];
                footer += " Исполнитель:__________________Ломанов В.И. " + "                                           Заказчик:_______________" + fa.Surname + " " + fa.FirstName + " " + fa.LastName;
            }

            ue = fodDO.RetrieveFOrderDetailsByOrderID(id);

            InitializeWorkbookFActSpecial(); //InitializeWorkbook();
            ISheet sheet1 = hssfworkbook.GetSheet("s");
            //create cell on rows, since rows do already exist,it's not necessary to create rows again.
            sheet1.GetRow(2).GetCell(3).SetCellValue(header);
            sheet1.GetRow(32).GetCell(3).SetCellValue(header);
            sheet1.GetRow(9).GetCell(0).SetCellValue(actNumber);
            sheet1.GetRow(39).GetCell(0).SetCellValue(actNumber);
            sheet1.GetRow(11).GetCell(0).SetCellValue(actNumber2);
            sheet1.GetRow(26).GetCell(0).SetCellValue(footer);
            //sheet1.GetRow(41).GetCell(0).SetCellValue(actNumber2);

            int rowC = 44;
            int row = 14;
            double sum = 0;
            double sumrub = 0;
            double getvat = 0;
            double finish = 0;

            int count = ue.Count;

            if (count <= 5)
            {
                CustomRetrieverDO crDO = new CustomRetrieverDO();
                ue = crDO.RetrieveFActByOrderID5Low(id);
                if (ue.Count > 0)
                {
                    for (int i = row; i < ue.Count + row; i++)
                    {
                        ArrayList al = (ArrayList)ue[i - row];
                        //FOrderDetailsAct u = (FOrderDetailsAct)fodal[i - row];

                        sheet1.GetRow(i).GetCell(0).SetCellValue(i - row + 1);
                        sheet1.GetRow(i).GetCell(1).SetCellValue(string.Format("Определения метрологических характеристик водомера  D{0}", al[0].ToString()));
                        sheet1.GetRow(i).GetCell(2).SetCellValue("калькуляция");
                        sheet1.GetRow(i).GetCell(3).SetCellValue("шт.");
                        sheet1.GetRow(i).GetCell(4).SetCellValue(al[4].ToString());
                        sheet1.GetRow(i).GetCell(5).SetCellValue(Convert.ToDouble(al[3]).ToString("0.00"));
                        sheet1.GetRow(i).GetCell(6).SetCellValue(Convert.ToDouble(al[3]).ToString("0.00"));


                        sum += Convert.ToDouble(al[3]);
                        rowC++;
                    }
                }
            }
            else
            {
                CustomRetrieverDO crDO = new CustomRetrieverDO();
                ue = crDO.RetrieveFActByOrderID5High(id);
                if (ue.Count > 0)
                 {
                    for (int i = row; i < ue.Count + row; i++)
                    {
                        ArrayList al = (ArrayList)ue[i - row];
                        //FOrderDetailsAct u = (FOrderDetailsAct)fodal[i - row];

                        sheet1.GetRow(i).GetCell(0).SetCellValue(i - row + 1);
                        sheet1.GetRow(i).GetCell(1).SetCellValue(string.Format("Определения метрологических характеристик водомера  D{0}", al[0].ToString()));
                        sheet1.GetRow(i).GetCell(2).SetCellValue("калькуляция");
                        sheet1.GetRow(i).GetCell(3).SetCellValue("шт.");
                        sheet1.GetRow(i).GetCell(4).SetCellValue(al[3].ToString());
                        sheet1.GetRow(i).GetCell(5).SetCellValue(Convert.ToDouble(al[1]).ToString("0.00"));
                        sheet1.GetRow(i).GetCell(6).SetCellValue(Convert.ToDouble(al[2]).ToString("0.00"));

                        sum += Convert.ToDouble(al[2]);
                        rowC++;
                    }
                }
            }
            
            sheet1.GetRow(19).GetCell(6).SetCellValue(sum.ToString("0.00"));
            sheet1.GetRow(20).GetCell(6).SetCellValue((sum / 100 * 20).ToString("0.00"));
            sheet1.GetRow(21).GetCell(6).SetCellValue((sum + (sum / 100 * 20)).ToString("0.00"));

            sheet1.GetRow(24).GetCell(GetLetterNumber("a")).SetCellValue("Сумма прописью: " + RuDateAndMoneyConverter.CurrencyToTxt((sum + Utilities.GetVAT(sum)), true));

            sumrub = sum;
            getvat = sumrub*0.2;
            getvat = Math.Round(getvat, 2);
            finish = (sumrub + getvat) + (sumrub + getvat);
            

            string tmp = string.Empty;
            tmp = sheet1.GetRow(29).GetCell(GetLetterNumber("c")).StringCellValue;

            string tmp1 = string.Empty;
            tmp1 = sheet1.GetRow(41).GetCell(GetLetterNumber("a")).StringCellValue;

            CustomRetrieverDO crdo = new CustomRetrieverDO();
            ue = crdo.RetrieveUserLocation(ULocationID);
            if (ue.Count > 0)
            {
                tmp1 = tmp1.Replace("257-42-61", (ue[0] as ArrayList)[2].ToString());
            }

            //tmp1 = tmp1.Replace("257-42-61", "");


            tmp = tmp.Replace("FIO", fa.Surname + " " + fa.FirstName + " " + fa.LastName);
            tmp = tmp.Replace("ADDRESS", fa.Address);
            tmp = tmp.Replace("PNONE", fa.Phone);
            tmp = tmp.Replace("DATE", DateTime.Now.ToString("dd MMMM yyyy"));
            tmp = tmp.Replace("NUMBER", fo.Prefix + fo.ID.ToString());
            tmp = tmp.Replace("TYPE", fo.ActionType);
            tmp = tmp.Replace("SUM", sum.ToString("0.00"));
            tmp = tmp.Replace("VAT", Utilities.GetVAT(sum).ToString("0.00"));
            tmp = tmp.Replace("VSEGO", (sum + Utilities.GetVAT(sum)).ToString("0.00"));
            tmp = tmp.Replace("ALL", finish.ToString("0.00"));



            sheet1.GetRow(41).GetCell(GetLetterNumber("a")).SetCellValue(tmp1);
            sheet1.GetRow(29).GetCell(GetLetterNumber("c")).SetCellValue(tmp);
            sheet1.GetRow(41).GetCell(GetLetterNumber("c")).SetCellValue(tmp);

            /*sheet1.GetRow(49).GetCell(6).SetCellValue(sum.ToString("0.00"));
            sheet1.GetRow(50).GetCell(6).SetCellValue((sum / 100 * 20).ToString("0.00"));
            sheet1.GetRow(51).GetCell(6).SetCellValue((sum + (sum / 100 * 20)).ToString("0.00"));*/

            //Force excel to recalculate all the formula while open
            sheet1.ForceFormulaRecalculation = true;

            WriteToFileFSpecial();
        }
        public static void GenerateBill(int id)
        {
            InitializeWorkbookBill();

            ISheet sheet1 = hssfworkbook.GetSheet("s");


            UniversalEntity ue = new UniversalEntity();
            //UniversalEntity uev = new UniversalEntity();

            UAbonent ua = new UAbonent();
            UAbonentDO uado = new UAbonentDO();
            UOrder uo = new UOrder();
            UOrderDO uodo = new UOrderDO();
            UOrderDetailsDO uodDO = new UOrderDetailsDO();

            ue = uado.RetrieveByOrderID(id);
            string header = string.Empty;
            string actNumber = string.Empty;
            if (ue.Count > 0)
            {
                ua = (UAbonent)ue[0];
                header += ua.Title + " \nюр адрес: " + ua.Address + " \n№ св. пл. НДС: " + ua.VATPay + " \n№ ИНН: " + ua.INN + " \nтел.: " + ua.Phone;
            }
            ue = uodo.RetrieveUOrderById(id);
            if (ue.Count > 0)
            {
                uo = (UOrder)ue[0];
                sheet1.GetRow(4).GetCell(GetLetterNumber("aj")).SetCellValue(uo.ID);
                sheet1.GetRow(9).GetCell(GetLetterNumber("ai")).SetCellValue(RuDateAndMoneyConverter.DateToTextLongUA(uo.DateIn) + "р.");


            }
            ue = uodDO.RetrieveUOrderDetailsByOrderID(id);
            sheet1.GetRow(16).GetCell(6).SetCellValue(header);



            int count = ue.Count;
            double sum = 0;
            int row = 20;
            if (count <= 5)
            {
                CustomRetrieverDO crDO = new CustomRetrieverDO();
                ue = crDO.RetrieveUActByOrderID5Low(id);
                if (ue.Count > 0)
                {
                    for (int i = row; i < ue.Count + row; i++)
                    {
                        ArrayList al = (ArrayList)ue[i - row];

                        sheet1.GetRow(i).GetCell(GetLetterNumber("a")).SetCellValue(string.Format(" *377;08;1 Определения метрологических характеристик водомера  D{0}", al[0].ToString()));
                        sheet1.GetRow(i).GetCell(GetLetterNumber("ad")).SetCellValue("шт.");
                        sheet1.GetRow(i).GetCell(GetLetterNumber("aj")).SetCellValue(al[4].ToString());
                        sheet1.GetRow(i).GetCell(GetLetterNumber("ap")).SetCellValue(Convert.ToDouble(al[3]).ToString("0.00"));
                        sheet1.GetRow(i).GetCell(GetLetterNumber("av")).SetCellValue(Convert.ToDouble(al[3]).ToString("0.00"));
                        sum += Convert.ToDouble(al[3]);
                    }
                }
            }
            else
            {
                CustomRetrieverDO crDO = new CustomRetrieverDO();
                ue = crDO.RetrieveUActByOrderID5High(id);
                if (ue.Count > 0)
                {
                    for (int i = row; i < ue.Count + row; i++)
                    {
                        ArrayList al = (ArrayList)ue[i - row];
                        //FOrderDetailsAct u = (FOrderDetailsAct)fodal[i - row];

                        sheet1.GetRow(i).GetCell(GetLetterNumber("a")).SetCellValue(string.Format(" *377;08;1 Определения метрологических характеристик водомера  D{0}", al[0].ToString()));
                        sheet1.GetRow(i).GetCell(GetLetterNumber("ad")).SetCellValue("шт.");
                        sheet1.GetRow(i).GetCell(GetLetterNumber("aj")).SetCellValue(al[3].ToString());
                        sheet1.GetRow(i).GetCell(GetLetterNumber("ap")).SetCellValue(Convert.ToDouble(al[1]).ToString("0.00"));
                        sheet1.GetRow(i).GetCell(GetLetterNumber("av")).SetCellValue(Convert.ToDouble(al[2]).ToString("0.00"));

                        sum += Convert.ToDouble(al[2]);
                    }
                }
            }
            /*int row = 20;
            double sum = 0;




            
            if (ue.Count > 0)
            {
                for (int i = row; i < ue.Count + row; i++)
                {
                    UOrderDetails u = (UOrderDetails)ue[i - row];

                    sheet1.GetRow(i).GetCell(GetLetterNumber("a")).SetCellValue("Определения метрологических характеристик водомера ");
                    sheet1.GetRow(i).GetCell(GetLetterNumber("ad")).SetCellValue("шт.");
                    sheet1.GetRow(i).GetCell(GetLetterNumber("aj")).SetCellValue("1");
                    sheet1.GetRow(i).GetCell(GetLetterNumber("ap")).SetCellValue(u.Price.ToString("0.00"));
                    sheet1.GetRow(i).GetCell(GetLetterNumber("av")).SetCellValue(u.Price.ToString("0.00"));
                    sum += u.Price;
                }
            }*/
            sheet1.GetRow(31).GetCell(GetLetterNumber("av")).SetCellValue(sum.ToString("0.00"));
            sheet1.GetRow(32).GetCell(GetLetterNumber("av")).SetCellValue(Utilities.GetVAT(sum).ToString("0.00"));
            sheet1.GetRow(33).GetCell(GetLetterNumber("av")).SetCellValue((sum + Utilities.GetVAT(sum)).ToString("0.00"));
            sheet1.GetRow(35).GetCell(GetLetterNumber("a")).SetCellValue("Загальна сума, що підлягає оплаті: " + UADateAndMoneyConverter.CurrencyToTxt((sum + Utilities.GetVAT(sum)), true));
            //Загальна сума, що підлягає оплаті


            //Force excel to recalculate all the formula while open
            sheet1.ForceFormulaRecalculation = true;

            WriteToFileB();
        }

        public static void GenerateReport(DataView dv)
        {

        }





        public static void GenerateAccountParser(List<AccountParser> lst)
        {
            InitializeWorkbookAccountParser();

            ISheet sheet1 = hssfworkbook.GetSheetAt(0);// GetSheet("s");

            foreach (AccountParser ap in lst)
            {
                try
                {

                    sheet1.GetRow(ap.n).CreateCell(8).SetCellValue(ap.d1);
                    sheet1.GetRow(ap.n).CreateCell(9).SetCellValue(ap.d2);
                }
                catch { }
            }

            /* sheet1.GetRow(31).GetCell(GetLetterNumber("av")).SetCellValue(sum.ToString("0.00"));
            sheet1.GetRow(32).GetCell(GetLetterNumber("av")).SetCellValue(Utilities.GetVAT(sum).ToString("0.00"));
            sheet1.GetRow(33).GetCell(GetLetterNumber("av")).SetCellValue((sum + Utilities.GetVAT(sum)).ToString("0.00"));
            sheet1.GetRow(35).GetCell(GetLetterNumber("a")).SetCellValue("Загальна сума, що підлягає оплаті: " + UADateAndMoneyConverter.CurrencyToTxt((sum + Utilities.GetVAT(sum)), true));
            */
            //Загальна сума, що підлягає оплаті


            //Force excel to recalculate all the formula while open
            sheet1.ForceFormulaRecalculation = true;

            WriteToFileAS();
        }

        static HSSFWorkbook hssfworkbook;

        static void WriteToFile()
        {
            //Write the stream data of workbook to the root directory
            string path = HttpContext.Current.Request.MapPath("~\\Templates/Uact.xls");
            FileStream file = new FileStream(path, FileMode.Create);
            hssfworkbook.Write(file);
            file.Close();
        }
        static void WriteToFileF()
        {
            //Write the stream data of workbook to the root directory
            string path = HttpContext.Current.Request.MapPath("~\\Templates/Fact.xls");
            FileStream file = new FileStream(path, FileMode.Create);
            hssfworkbook.Write(file);
            file.Close();
        }

        static void WriteToFileWithdrawal()
        {
            //Write the stream data of workbook to the root directory
            string path = HttpContext.Current.Request.MapPath("~\\Templates/WithdrawalAct.xls");
            FileStream file = new FileStream(path, FileMode.Create);
            hssfworkbook.Write(file);
            file.Close();
        }

        static void WriteToFileWithdrawalSpecial()
        {
            //Write the stream data of workbook to the root directory
            string path = HttpContext.Current.Request.MapPath("~\\Templates/AlternativeAct.xls");
            FileStream file = new FileStream(path, FileMode.Create);
            hssfworkbook.Write(file);
            file.Close();
        }

        static void WriteToFileReplacementAct()
        {
            //Write the stream data of workbook to the root directory
            string path = HttpContext.Current.Request.MapPath("~\\Templates/ReplacementAct.xls");
            FileStream file = new FileStream(path, FileMode.Create);
            hssfworkbook.Write(file);
            file.Close();
        }

        static void WriteToFileRubF()
        {
            string path = HttpContext.Current.Request.MapPath("~\\Templates/RubFact.xls");
            FileStream file = new FileStream(path, FileMode.Create);
            hssfworkbook.Write(file);
            file.Close();
        }

        static void WriteToFileRubFOld()
        {
            string path = HttpContext.Current.Request.MapPath("~\\Templates/FActOld.xls");
            FileStream file = new FileStream(path, FileMode.Create);
            hssfworkbook.Write(file);
            file.Close();
        }


        static void WriteToFileUARub()
        {
            //Write the stream data of workbook to the root directory
            string path = HttpContext.Current.Request.MapPath("~\\Templates/UactRub.xls");
            FileStream file = new FileStream(path, FileMode.Create);
            hssfworkbook.Write(file);
            file.Close();
        }

        static void WriteToFileUARubles()
        {
            //Write the stream data of workbook to the root directory
            string path = HttpContext.Current.Request.MapPath("~\\Templates/UactRubles.xls");
            FileStream file = new FileStream(path, FileMode.Create);
            hssfworkbook.Write(file);
            file.Close();
        }

        static void WriteToFileUARublesForLittleWDK()
        {
            //Write the stream data of workbook to the root directory
            string path = HttpContext.Current.Request.MapPath("~\\Templates/LittleWDKActRubles.xls");
            FileStream file = new FileStream(path, FileMode.Create);
            hssfworkbook.Write(file);
            file.Close();
        }

        static void WriteToFileFSpecial()
        {
            //Write the stream data of workbook to the root directory
            string path = HttpContext.Current.Request.MapPath("~\\Templates/out act_check.xls");
            FileStream file = new FileStream(path, FileMode.Create);
            hssfworkbook.Write(file);
            file.Close();
        }

        static void WriteToFileFSpeciaOld()
        {
            //Write the stream data of workbook to the root directory
            string path = HttpContext.Current.Request.MapPath("~\\Templates/FActCheckOld.xls");
            FileStream file = new FileStream(path, FileMode.Create);
            hssfworkbook.Write(file);
            file.Close();
        }

        static void WriteToFileFSpecialRub()
        {
            //Write the stream data of workbook to the root directory
            string path = HttpContext.Current.Request.MapPath("~\\Templates/out act_check_rub.xls");
            FileStream file = new FileStream(path, FileMode.Create);
            hssfworkbook.Write(file);
            file.Close();
        }
        static void WriteToFileB()
        {
            //Write the stream data of workbook to the root directory
            string path = HttpContext.Current.Request.MapPath("~\\Templates/outBill.xls");
            FileStream file = new FileStream(path, FileMode.Create);
            hssfworkbook.Write(file);
            file.Close();
        }

        static void WriteToFileBRubles()
        {
            //Write the stream data of workbook to the root directory
            string path = HttpContext.Current.Request.MapPath("~\\Templates/outBillRubles.xls");
            FileStream file = new FileStream(path, FileMode.Create);
            hssfworkbook.Write(file);
            file.Close();
        }

        static void WriteToFileBRub()
        {
            //Write the stream data of workbook to the root directory
            string path = HttpContext.Current.Request.MapPath("~\\Templates/outBillRub.xls");
            FileStream file = new FileStream(path, FileMode.Create);
            hssfworkbook.Write(file);
            file.Close();
        }
        static void WriteToFileAS()
        {
            //Write the stream data of workbook to the root directory
            string path = HttpContext.Current.Request.MapPath("/outimport.xls");
            FileStream file = new FileStream(path, FileMode.Create);
            hssfworkbook.Write(file);
            file.Close();
        }

        static void WriteToFileBillForLittleWDK()
        {
            //Write the stream data of workbook to the root directory
            string path = HttpContext.Current.Request.MapPath("~\\Templates/littlewdkbillrubles.xls");
            FileStream file = new FileStream(path, FileMode.Create);
            hssfworkbook.Write(file);
            file.Close();
        }

        static void InitializeWorkbook()
        {
            //read the template via FileStream, it is suggested to use FileAccess.Read to prevent file lock.
            //book1.xls is an Excel-2007-generated file, so some new unknown BIFF records are added. 
            string path = HttpContext.Current.Request.MapPath("~\\Templates/act.xls");
            FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);

            hssfworkbook = new HSSFWorkbook(file);

            //create a entry of DocumentSummaryInformation
            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = "NPOI Team";
            hssfworkbook.DocumentSummaryInformation = dsi;

            //create a entry of SummaryInformation
            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Subject = "NPOI SDK Example";
            hssfworkbook.SummaryInformation = si;
        }

        static void InitializeWorkBookUActRub()
        {
            //read the template via FileStream, it is suggested to use FileAccess.Read to prevent file lock.
            //book1.xls is an Excel-2007-generated file, so some new unknown BIFF records are added. 
            string path = HttpContext.Current.Request.MapPath("~\\Templates/uact_rub.xls");
            FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);

            hssfworkbook = new HSSFWorkbook(file);

            //create a entry of DocumentSummaryInformation
            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = "NPOI Team";
            hssfworkbook.DocumentSummaryInformation = dsi;

            //create a entry of SummaryInformation
            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Subject = "NPOI SDK Example";
            hssfworkbook.SummaryInformation = si;
        }

        static void InitializeWorkBookUActRubles()
        {
            //read the template via FileStream, it is suggested to use FileAccess.Read to prevent file lock.
            //book1.xls is an Excel-2007-generated file, so some new unknown BIFF records are added. 
            string path = HttpContext.Current.Request.MapPath("~\\Templates/uact_rubles.xls");
            FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);

            hssfworkbook = new HSSFWorkbook(file);

            //create a entry of DocumentSummaryInformation
            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = "NPOI Team";
            hssfworkbook.DocumentSummaryInformation = dsi;

            //create a entry of SummaryInformation
            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Subject = "NPOI SDK Example";
            hssfworkbook.SummaryInformation = si;
        }

        static void InitializeWorkbookFAct()
        {
            //read the template via FileStream, it is suggested to use FileAccess.Read to prevent file lock.
            //book1.xls is an Excel-2007-generated file, so some new unknown BIFF records are added. 
            string path = HttpContext.Current.Request.MapPath("~\\Templates/act2.xls");
            FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);

            hssfworkbook = new HSSFWorkbook(file);

            //create a entry of DocumentSummaryInformation
            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = "NPOI Team";
            hssfworkbook.DocumentSummaryInformation = dsi;

            //create a entry of SummaryInformation
            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Subject = "NPOI SDK Example";
            hssfworkbook.SummaryInformation = si;
        }
        static void InitializeWorkbookFActRub()
        {
            //read the template via FileStream, it is suggested to use FileAccess.Read to prevent file lock.
            //book1.xls is an Excel-2007-generated file, so some new unknown BIFF records are added. 
            string path = HttpContext.Current.Request.MapPath("~\\Templates/act2_rub.xls");
            FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);

            hssfworkbook = new HSSFWorkbook(file);

            //create a entry of DocumentSummaryInformation
            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = "NPOI Team";
            hssfworkbook.DocumentSummaryInformation = dsi;

            //create a entry of SummaryInformation
            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Subject = "NPOI SDK Example";
            hssfworkbook.SummaryInformation = si;
        }

        static void InitializeWorkbookFActRubOld()
        {
            //read the template via FileStream, it is suggested to use FileAccess.Read to prevent file lock.
            //book1.xls is an Excel-2007-generated file, so some new unknown BIFF records are added. 
            string path = HttpContext.Current.Request.MapPath("~\\Templates/act2_old.xls");
            FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);

            hssfworkbook = new HSSFWorkbook(file);

            //create a entry of DocumentSummaryInformation
            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = "NPOI Team";
            hssfworkbook.DocumentSummaryInformation = dsi;

            //create a entry of SummaryInformation
            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Subject = "NPOI SDK Example";
            hssfworkbook.SummaryInformation = si;
        }

        static void InitializeWorkbookWithdrawalAct()
        {
            //read the template via FileStream, it is suggested to use FileAccess.Read to prevent file lock.
            //book1.xls is an Excel-2007-generated file, so some new unknown BIFF records are added. 
            string path = HttpContext.Current.Request.MapPath("~\\Templates/withdrawalact_rub.xls");
            FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);

            hssfworkbook = new HSSFWorkbook(file);

            //create a entry of DocumentSummaryInformation
            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = "NPOI Team";
            hssfworkbook.DocumentSummaryInformation = dsi;

            //create a entry of SummaryInformation
            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Subject = "NPOI SDK Example";
            hssfworkbook.SummaryInformation = si;
        }

        static void InitializeWorkbookReplacementAct()
        {
            //read the template via FileStream, it is suggested to use FileAccess.Read to prevent file lock.
            //book1.xls is an Excel-2007-generated file, so some new unknown BIFF records are added. 
            string path = HttpContext.Current.Request.MapPath("~\\Templates/alternreplace.xls");
            FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);

            hssfworkbook = new HSSFWorkbook(file);

            //create a entry of DocumentSummaryInformation
            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = "NPOI Team";
            hssfworkbook.DocumentSummaryInformation = dsi;

            //create a entry of SummaryInformation
            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Subject = "NPOI SDK Example";
            hssfworkbook.SummaryInformation = si;
        } 

        static void InitializeWorkbookWithdrawalActSpecial()
        {
            //read the template via FileStream, it is suggested to use FileAccess.Read to prevent file lock.
            //book1.xls is an Excel-2007-generated file, so some new unknown BIFF records are added. 
            string path = HttpContext.Current.Request.MapPath("~\\Templates/altern.xls");
            FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);

            hssfworkbook = new HSSFWorkbook(file);

            //create a entry of DocumentSummaryInformation
            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = "NPOI Team";
            hssfworkbook.DocumentSummaryInformation = dsi;

            //create a entry of SummaryInformation
            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Subject = "NPOI SDK Example";
            hssfworkbook.SummaryInformation = si;
        }

        static void InitializeWorkbookFActSpecial()
        {
            //read the template via FileStream, it is suggested to use FileAccess.Read to prevent file lock.
            //book1.xls is an Excel-2007-generated file, so some new unknown BIFF records are added. 
            string path = HttpContext.Current.Request.MapPath("~\\Templates/act_check.xls");
            FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);

            hssfworkbook = new HSSFWorkbook(file);

            //create a entry of DocumentSummaryInformation
            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = "NPOI Team";
            hssfworkbook.DocumentSummaryInformation = dsi;

            //create a entry of SummaryInformation
            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Subject = "NPOI SDK Example";
            hssfworkbook.SummaryInformation = si;
        }

        static void InitializeWorkBookFActSpecialRub()
        {
            //read the template via FileStream, it is suggested to use FileAccess.Read to prevent file lock.
            //book1.xls is an Excel-2007-generated file, so some new unknown BIFF records are added. 
            string path = HttpContext.Current.Request.MapPath("~\\Templates/act_check_rub.xls");
            FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);

            hssfworkbook = new HSSFWorkbook(file);

            //create a entry of DocumentSummaryInformation
            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = "NPOI Team";
            hssfworkbook.DocumentSummaryInformation = dsi;

            //create a entry of SummaryInformation
            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Subject = "NPOI SDK Example";
            hssfworkbook.SummaryInformation = si;
        }

        static void InitializeWorkBookFActSpecialOld()
        {
            //read the template via FileStream, it is suggested to use FileAccess.Read to prevent file lock.
            //book1.xls is an Excel-2007-generated file, so some new unknown BIFF records are added. 
            string path = HttpContext.Current.Request.MapPath("~\\Templates/act_check_old.xls");
            FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);

            hssfworkbook = new HSSFWorkbook(file);

            //create a entry of DocumentSummaryInformation
            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = "NPOI Team";
            hssfworkbook.DocumentSummaryInformation = dsi;

            //create a entry of SummaryInformation
            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Subject = "NPOI SDK Example";
            hssfworkbook.SummaryInformation = si;
        }
        static void InitializeWorkbookBill()
        {
            //read the template via FileStream, it is suggested to use FileAccess.Read to prevent file lock.
            //book1.xls is an Excel-2007-generated file, so some new unknown BIFF records are added. 
            string path = HttpContext.Current.Request.MapPath("~\\Templates/Bill.xls");
            FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);

            hssfworkbook = new HSSFWorkbook(file);

            //create a entry of DocumentSummaryInformation
            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = "NPOI Team";
            hssfworkbook.DocumentSummaryInformation = dsi;

            //create a entry of SummaryInformation
            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Subject = "NPOI SDK Example";
            hssfworkbook.SummaryInformation = si;
        }

        static void InitializeWorkbookBillRub()
        {
            //read the template via FileStream, it is suggested to use FileAccess.Read to prevent file lock.
            //book1.xls is an Excel-2007-generated file, so some new unknown BIFF records are added. 
            string path = HttpContext.Current.Request.MapPath("~\\Templates/BillRub.xls");
            FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);

            hssfworkbook = new HSSFWorkbook(file);

            //create a entry of DocumentSummaryInformation
            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = "NPOI Team";
            hssfworkbook.DocumentSummaryInformation = dsi;

            //create a entry of SummaryInformation
            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Subject = "NPOI SDK Example";
            hssfworkbook.SummaryInformation = si;
        }

        static void InitializeWorkbookBillRubles()
        {
            //read the template via FileStream, it is suggested to use FileAccess.Read to prevent file lock.
            //book1.xls is an Excel-2007-generated file, so some new unknown BIFF records are added. 
            string path = HttpContext.Current.Request.MapPath("~\\Templates/BillPayRubles.xls");
            FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);

            hssfworkbook = new HSSFWorkbook(file);

            //create a entry of DocumentSummaryInformation
            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = "NPOI Team";
            hssfworkbook.DocumentSummaryInformation = dsi;

            //create a entry of SummaryInformation
            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Subject = "NPOI SDK Example";
            hssfworkbook.SummaryInformation = si;
        }

        static void InitializeWorkbookBillForLittleWDK()
        {
            //read the template via FileStream, it is suggested to use FileAccess.Read to prevent file lock.
            //book1.xls is an Excel-2007-generated file, so some new unknown BIFF records are added. 
            string path = HttpContext.Current.Request.MapPath("~\\Templates/BillForLittleWDK.xls");
            FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);

            hssfworkbook = new HSSFWorkbook(file);

            //create a entry of DocumentSummaryInformation
            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = "NPOI Team";
            hssfworkbook.DocumentSummaryInformation = dsi;

            //create a entry of SummaryInformation
            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Subject = "NPOI SDK Example";
            hssfworkbook.SummaryInformation = si;
        }

        static void InitializeWorkBookUActRublesForLittleWDK()
        {
            //read the template via FileStream, it is suggested to use FileAccess.Read to prevent file lock.
            //book1.xls is an Excel-2007-generated file, so some new unknown BIFF records are added. 
            string path = HttpContext.Current.Request.MapPath("~\\Templates/uactRublesForLittleWDK.xls");
            FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);

            hssfworkbook = new HSSFWorkbook(file);

            //create a entry of DocumentSummaryInformation
            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = "NPOI Team";
            hssfworkbook.DocumentSummaryInformation = dsi;

            //create a entry of SummaryInformation
            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Subject = "NPOI SDK Example";
            hssfworkbook.SummaryInformation = si;
        }


        static void InitializeWorkbookAccountParser()
        {
            //read the template via FileStream, it is suggested to use FileAccess.Read to prevent file lock.
            //book1.xls is an Excel-2007-generated file, so some new unknown BIFF records are added. 
            string path = HttpContext.Current.Request.MapPath("/import.xls");
            FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);

            hssfworkbook = new HSSFWorkbook(file);

            //create a entry of DocumentSummaryInformation
            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = "NPOI Team";
            hssfworkbook.DocumentSummaryInformation = dsi;

            //create a entry of SummaryInformation
            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Subject = "NPOI SDK Example";
            hssfworkbook.SummaryInformation = si;
        }


        //
        //
        //
        //
        //РУБЛЕВЫЙ СЧЕТ
        //
        //
        //


        //генерация рублевого акта для физ лиц
        public static void GenerateFActRub(int id)
        {
            UniversalEntity ue = new UniversalEntity();
            string header = string.Empty;
            string actNumber = string.Empty;
            string actNumber2 = string.Empty;
            string footer = string.Empty;

            FAbonentDO faDO = new FAbonentDO();

            ue = faDO.RetrieveByOrderID(id);
            if (ue.Count > 0)
            {
                FAbonent fa = (FAbonent)ue[0];
                header += fa.Surname + " " + fa.FirstName + " " + fa.LastName + " \nадрес: " + fa.Address + "\nтел.: " + fa.Phone;
            }

            FOrder fo = new FOrder();
            FOrderDO foDO = new FOrderDO();
            FOrderDetailsDO fodDO = new FOrderDetailsDO();

            ue = foDO.RetrieveFOrderById(id);
            if (ue.Count > 0)
            {
                fo = (FOrder)ue[0];
                actNumber = "  г. Донецк            АКТ № " + fo.Prefix + fo.ID + " за _________________________________г.";
                actNumber2 = "                             по договору (письму) №  " + fo.Prefix + fo.ID + " от " + RuDateAndMoneyConverter.DateToTextLong(fo.DateIn) + " г.";
            }

            ue = faDO.RetrieveByOrderID(id);
            if (ue.Count > 0)
            {
                FAbonent fa = (FAbonent) ue[0];
                footer += " Исполнитель:__________________Ломанов В.И. " + "                                           Заказчик:_______________" + fa.Surname + " " + fa.FirstName + " " + fa.LastName;
            }

            ue = fodDO.RetrieveFOrderDetailsByOrderID(id);

            InitializeWorkbookFActRub(); //InitializeWorkbook();
            ISheet sheet1 = hssfworkbook.GetSheet("s");
            //create cell on rows, since rows do already exist,it's not necessary to create rows again.
            sheet1.GetRow(2).GetCell(3).SetCellValue(header);
            sheet1.GetRow(32).GetCell(3).SetCellValue(header);
            sheet1.GetRow(9).GetCell(0).SetCellValue(actNumber);
            sheet1.GetRow(39).GetCell(0).SetCellValue(actNumber);
            sheet1.GetRow(11).GetCell(0).SetCellValue(actNumber2);
            sheet1.GetRow(41).GetCell(0).SetCellValue(actNumber2);
            sheet1.GetRow(29).GetCell(0).SetCellValue(footer);
            sheet1.GetRow(59).GetCell(0).SetCellValue(footer);

            int rowC = 44;
            int row = 14;
            double sum = 0;
            double grn = 0;
            double getvatrub = 0;
            double sumrub = 0;
            double getvat = 0;
            double finish = 0;

            int count = ue.Count;

            if (count <= 5)
            {
                CustomRetrieverDO crDO = new CustomRetrieverDO();
                ue = crDO.RetrieveFActByOrderID5LowRub(id);
                if (ue.Count > 0)
                {
                    for (int i = row; i < ue.Count + row; i++)
                    {
                        ArrayList al = (ArrayList)ue[i - row];
                        //FOrderDetailsAct u = (FOrderDetailsAct)fodal[i - row];
                        /*            sumrub = sum;
            getvat = sumrub*0.2;
            getvat = Math.Round(getvat, 2);
            finish = (sumrub + getvat) + (sumrub + getvat);*/

                        double price = 0;
                        double nds = 0;
                        double givememoney = 0;

                        price += Convert.ToDouble(al[3]);
                        nds = price*0.2;
                        nds = Math.Round(nds, 2);
                        givememoney = (price + nds) + (price + nds);

                        sheet1.GetRow(i).GetCell(0).SetCellValue(i - row + 1);
                        sheet1.GetRow(i).GetCell(1).SetCellValue(string.Format("Определения метрологических характеристик водомера  D{0}", al[0].ToString()));
                        sheet1.GetRow(i).GetCell(2).SetCellValue("калькуляция");
                        sheet1.GetRow(i).GetCell(3).SetCellValue("шт.");
                        sheet1.GetRow(i).GetCell(4).SetCellValue(al[4].ToString());
                        sheet1.GetRow(i).GetCell(5).SetCellValue(Convert.ToDouble(al[3]).ToString("0.00"));
                        sheet1.GetRow(i).GetCell(6).SetCellValue(Convert.ToDouble(al[3]).ToString("0.00"));
                        //sheet1.GetRow(i).GetCell(5).SetCellValue(givememoney.ToString("0.00"));
                        //sheet1.GetRow(i).GetCell(6).SetCellValue(givememoney.ToString("0.00"));


                        sheet1.GetRow(rowC).GetCell(0).SetCellValue(i - row + 1);
                        sheet1.GetRow(rowC).GetCell(1).SetCellValue(string.Format("Определения метрологических характеристик водомера  D{0}", al[0].ToString()));
                        sheet1.GetRow(rowC).GetCell(2).SetCellValue("калькуляция");
                        sheet1.GetRow(rowC).GetCell(3).SetCellValue("шт.");
                        sheet1.GetRow(rowC).GetCell(4).SetCellValue(al[4].ToString());
                        sheet1.GetRow(rowC).GetCell(5).SetCellValue(Convert.ToDouble(al[3]).ToString("0.00"));
                        sheet1.GetRow(rowC).GetCell(6).SetCellValue(Convert.ToDouble(al[3]).ToString("0.00"));
                       // sheet1.GetRow(rowC).GetCell(5).SetCellValue(givememoney.ToString("0.00"));
                        //sheet1.GetRow(rowC).GetCell(6).SetCellValue(givememoney.ToString("0.00"));


                        sum += Convert.ToDouble(al[3]);
                        rowC++;
                    }
                }
            }
            else
            {
                CustomRetrieverDO crDO = new CustomRetrieverDO();
                ue = crDO.RetrieveFActByOrderID5HighRub(id);
                if (ue.Count > 0)
                {
                    for (int i = row; i < ue.Count + row; i++)
                    {
                        ArrayList al = (ArrayList)ue[i - row];
                        //FOrderDetailsAct u = (FOrderDetailsAct)fodal[i - row];

                        //рублевый счет/блок для al[1]
                        double price = 0;
                        double nds = 0;
                        double givememoney = 0;

                        price += Convert.ToDouble(al[1]);
                        nds = price*0.2;
                        nds = Math.Round(nds, 2);
                        givememoney = (price + nds) + (price + nds);


                        //рублевый счет/блок для al[2]
                        double business = 0;
                        double makebusiness = 0;
                        double somemoney = 0;

                        business += Convert.ToDouble(al[2]);
                        makebusiness = business*0.2;
                        makebusiness = Math.Round(makebusiness, 2);
                        somemoney = (business + makebusiness) + (business + makebusiness);
                         

                        sheet1.GetRow(i).GetCell(0).SetCellValue(i - row + 1);
                        sheet1.GetRow(i).GetCell(1).SetCellValue(string.Format("Определения метрологических характеристик водомера  D{0}", al[0].ToString()));
                        sheet1.GetRow(i).GetCell(2).SetCellValue("калькуляция");
                        sheet1.GetRow(i).GetCell(3).SetCellValue("шт.");
                        sheet1.GetRow(i).GetCell(4).SetCellValue(al[3].ToString());
                        sheet1.GetRow(i).GetCell(5).SetCellValue(Convert.ToDouble(al[1]).ToString("0.00"));
                        sheet1.GetRow(i).GetCell(6).SetCellValue(Convert.ToDouble(al[2]).ToString("0.00"));


                        sheet1.GetRow(rowC).GetCell(0).SetCellValue(i - row + 1);
                        sheet1.GetRow(rowC).GetCell(1).SetCellValue(string.Format("Определения метрологических характеристик водомера  D{0}", al[0].ToString()));
                        sheet1.GetRow(rowC).GetCell(2).SetCellValue("калькуляция");
                        sheet1.GetRow(rowC).GetCell(3).SetCellValue("шт.");
                        sheet1.GetRow(rowC).GetCell(4).SetCellValue(al[3].ToString());
                        sheet1.GetRow(rowC).GetCell(5).SetCellValue(Convert.ToDouble(al[1]).ToString("0.00"));
                        sheet1.GetRow(rowC).GetCell(6).SetCellValue(Convert.ToDouble(al[2]).ToString("0.00"));

                        sum += Convert.ToDouble(al[2]);
                        rowC++;
                    }
                }
            }

            //рублевый счет для вывода итоговый суммы
            sumrub = sum;
            getvat = sumrub*0.2;
            getvat = Math.Round(getvat, 2);
            finish = (sumrub + getvat) + (sumrub + getvat);
            //для вывода ндс и цены рублевых
            grn = sum*2;
            getvatrub = grn*0.2;

           // sheet1.GetRow(19).GetCell(6).SetCellValue(grn.ToString("0.00"));
            //sheet1.GetRow(20).GetCell(6).SetCellValue(getvatrub.ToString("0.00"));
            sheet1.GetRow(21).GetCell(6).SetCellValue(sumrub.ToString("0.00"));

            //sheet1.GetRow(49).GetCell(6).SetCellValue(grn.ToString("0.00"));
            //sheet1.GetRow(50).GetCell(6).SetCellValue(getvatrub.ToString("0.00"));
            sheet1.GetRow(51).GetCell(6).SetCellValue(sumrub.ToString("0.00"));

            //sheet1.GetRow(35).GetCell(GetLetterNumber("a")).SetCellValue("Загальна сума, що підлягає оплаті: " + UADateAndMoneyConverter.CurrencyToTxt((sum + Utilities.GetVAT(sum)), true));
            sheet1.GetRow(24).GetCell(GetLetterNumber("a")).SetCellValue("Сумма прописью: " + RuDateAndMoneyConverter.CurrencyToTxtRub(sumrub, true));
            sheet1.GetRow(54).GetCell(GetLetterNumber("a")).SetCellValue("Сумма прописью: " + RuDateAndMoneyConverter.CurrencyToTxtRub(sumrub, true));


            //Force excel to recalculate all the formula while open
            sheet1.ForceFormulaRecalculation = true;

            WriteToFileRubF();
        }

        public static void GenerateFActOld(int id)
        {
            UniversalEntity ue = new UniversalEntity();
            string header = string.Empty;
            string actNumber = string.Empty;
            string actNumber2 = string.Empty;
            string footer = string.Empty;

            FAbonentDO faDO = new FAbonentDO();

            ue = faDO.RetrieveByOrderID(id);
            if (ue.Count > 0)
            {
                FAbonent fa = (FAbonent)ue[0];
                header += fa.Surname + " " + fa.FirstName + " " + fa.LastName + " \nадрес: " + fa.Address + "\nтел.: " + fa.Phone;
            }

            FOrder fo = new FOrder();
            FOrderDO foDO = new FOrderDO();
            FOrderDetailsDO fodDO = new FOrderDetailsDO();

            ue = foDO.RetrieveFOrderById(id);
            if (ue.Count > 0)
            {
                fo = (FOrder)ue[0];
                actNumber = "  г. Донецк            АКТ № " + fo.Prefix + fo.ID + " за _________________________________г.";
                actNumber2 = "                             по договору (письму) №  " + fo.Prefix + fo.ID + " от " + RuDateAndMoneyConverter.DateToTextLong(fo.DateIn) + " г.";
            }

            ue = faDO.RetrieveByOrderID(id);
            if (ue.Count > 0)
            {
                FAbonent fa = (FAbonent)ue[0];
                footer += " Исполнитель:__________________Ломанов В.И. " + "                                           Заказчик:_______________" + fa.Surname + " " + fa.FirstName + " " + fa.LastName;
            }

            ue = fodDO.RetrieveFOrderDetailsByOrderID(id);

            InitializeWorkbookFActRubOld(); //InitializeWorkbook();
            ISheet sheet1 = hssfworkbook.GetSheet("s");
            //create cell on rows, since rows do already exist,it's not necessary to create rows again.
            sheet1.GetRow(2).GetCell(3).SetCellValue(header);
            sheet1.GetRow(32).GetCell(3).SetCellValue(header);
            sheet1.GetRow(9).GetCell(0).SetCellValue(actNumber);
            sheet1.GetRow(39).GetCell(0).SetCellValue(actNumber);
            sheet1.GetRow(11).GetCell(0).SetCellValue(actNumber2);
            sheet1.GetRow(41).GetCell(0).SetCellValue(actNumber2);
            sheet1.GetRow(26).GetCell(0).SetCellValue(footer);
            sheet1.GetRow(56).GetCell(0).SetCellValue(footer);

            int rowC = 44;
            int row = 14;
            double sum = 0;
            double grn = 0;
            double getvatrub = 0;
            double sumrub = 0;
            double getvat = 0;
            double finish = 0;

            int count = ue.Count;

            if (count <= 5)
            {
                CustomRetrieverDO crDO = new CustomRetrieverDO();
                ue = crDO.RetrieveFActByOrderID5Low(id);
                if (ue.Count > 0)
                {
                    for (int i = row; i < ue.Count + row; i++)
                    {
                        ArrayList al = (ArrayList)ue[i - row];
                        //FOrderDetailsAct u = (FOrderDetailsAct)fodal[i - row];
                        /*            sumrub = sum;
            getvat = sumrub*0.2;
            getvat = Math.Round(getvat, 2);
            finish = (sumrub + getvat) + (sumrub + getvat);*/

                        double price = 0;
                        double nds = 0;
                        double givememoney = 0;

                        price += Convert.ToDouble(al[3]);
                        nds = price * 0.2;
                        nds = Math.Round(nds, 2);
                        givememoney = (price + nds) + (price + nds);

                        sheet1.GetRow(i).GetCell(0).SetCellValue(i - row + 1);
                        sheet1.GetRow(i).GetCell(1).SetCellValue(string.Format("Определения метрологических характеристик водомера  D{0}", al[0].ToString()));
                        sheet1.GetRow(i).GetCell(2).SetCellValue("калькуляция");
                        sheet1.GetRow(i).GetCell(3).SetCellValue("шт.");
                        sheet1.GetRow(i).GetCell(4).SetCellValue(al[4].ToString());
                        //sheet1.GetRow(i).GetCell(5).SetCellValue(Convert.ToDouble(al[3]).ToString("0.00"));
                        //sheet1.GetRow(i).GetCell(6).SetCellValue(Convert.ToDouble(al[3]).ToString("0.00"));
                        sheet1.GetRow(i).GetCell(5).SetCellValue(givememoney.ToString("0.00"));
                        sheet1.GetRow(i).GetCell(6).SetCellValue(givememoney.ToString("0.00"));


                        sheet1.GetRow(rowC).GetCell(0).SetCellValue(i - row + 1);
                        sheet1.GetRow(rowC).GetCell(1).SetCellValue(string.Format("Определения метрологических характеристик водомера  D{0}", al[0].ToString()));
                        sheet1.GetRow(rowC).GetCell(2).SetCellValue("калькуляция");
                        sheet1.GetRow(rowC).GetCell(3).SetCellValue("шт.");
                        sheet1.GetRow(rowC).GetCell(4).SetCellValue(al[4].ToString());
                        //sheet1.GetRow(rowC).GetCell(5).SetCellValue(Convert.ToDouble(al[3]).ToString("0.00"));
                        //sheet1.GetRow(rowC).GetCell(6).SetCellValue(Convert.ToDouble(al[3]).ToString("0.00"));
                        sheet1.GetRow(rowC).GetCell(5).SetCellValue(givememoney.ToString("0.00"));
                        sheet1.GetRow(rowC).GetCell(6).SetCellValue(givememoney.ToString("0.00"));


                        sum += Convert.ToDouble(al[3]);
                        rowC++;
                    }
                }
            }
            else
            {
                CustomRetrieverDO crDO = new CustomRetrieverDO();
                ue = crDO.RetrieveFActByOrderID5High(id);
                if (ue.Count > 0)
                {
                    for (int i = row; i < ue.Count + row; i++)
                    {
                        ArrayList al = (ArrayList)ue[i - row];
                        //FOrderDetailsAct u = (FOrderDetailsAct)fodal[i - row];

                        //рублевый счет/блок для al[1]
                        double price = 0;
                        double nds = 0;
                        double givememoney = 0;

                        price += Convert.ToDouble(al[1]);
                        nds = price * 0.2;
                        nds = Math.Round(nds, 2);
                        givememoney = (price + nds) + (price + nds);


                        //рублевый счет/блок для al[2]
                        double business = 0;
                        double makebusiness = 0;
                        double somemoney = 0;

                        business += Convert.ToDouble(al[2]);
                        makebusiness = business * 0.2;
                        makebusiness = Math.Round(makebusiness, 2);
                        somemoney = (business + makebusiness) + (business + makebusiness);


                        sheet1.GetRow(i).GetCell(0).SetCellValue(i - row + 1);
                        sheet1.GetRow(i).GetCell(1).SetCellValue(string.Format("Определения метрологических характеристик водомера  D{0}", al[0].ToString()));
                        sheet1.GetRow(i).GetCell(2).SetCellValue("калькуляция");
                        sheet1.GetRow(i).GetCell(3).SetCellValue("шт.");
                        sheet1.GetRow(i).GetCell(4).SetCellValue(al[3].ToString());
                        sheet1.GetRow(i).GetCell(5).SetCellValue(givememoney.ToString("0.00"));
                        sheet1.GetRow(i).GetCell(6).SetCellValue(somemoney.ToString("0.00"));


                        sheet1.GetRow(rowC).GetCell(0).SetCellValue(i - row + 1);
                        sheet1.GetRow(rowC).GetCell(1).SetCellValue(string.Format("Определения метрологических характеристик водомера  D{0}", al[0].ToString()));
                        sheet1.GetRow(rowC).GetCell(2).SetCellValue("калькуляция");
                        sheet1.GetRow(rowC).GetCell(3).SetCellValue("шт.");
                        sheet1.GetRow(rowC).GetCell(4).SetCellValue(al[3].ToString());
                        sheet1.GetRow(rowC).GetCell(5).SetCellValue(givememoney.ToString("0.00"));
                        sheet1.GetRow(rowC).GetCell(6).SetCellValue(somemoney.ToString("0.00"));

                        sum += Convert.ToDouble(al[2]);
                        rowC++;
                    }
                }
            }

            //рублевый счет для вывода итоговый суммы
            sumrub = sum;
            getvat = sumrub * 0.2;
            getvat = Math.Round(getvat, 2);
            finish = (sumrub + getvat) + (sumrub + getvat);
            //для вывода ндс и цены рублевых
            grn = sum * 2;
            getvatrub = grn * 0.2;

            // sheet1.GetRow(19).GetCell(6).SetCellValue(grn.ToString("0.00"));
            //sheet1.GetRow(20).GetCell(6).SetCellValue(getvatrub.ToString("0.00"));
            sheet1.GetRow(21).GetCell(6).SetCellValue(finish.ToString("0.00"));

            //sheet1.GetRow(49).GetCell(6).SetCellValue(grn.ToString("0.00"));
            //sheet1.GetRow(50).GetCell(6).SetCellValue(getvatrub.ToString("0.00"));
            sheet1.GetRow(51).GetCell(6).SetCellValue(finish.ToString("0.00"));

            //sheet1.GetRow(35).GetCell(GetLetterNumber("a")).SetCellValue("Загальна сума, що підлягає оплаті: " + UADateAndMoneyConverter.CurrencyToTxt((sum + Utilities.GetVAT(sum)), true));
            sheet1.GetRow(24).GetCell(GetLetterNumber("a")).SetCellValue("Сумма прописью: " + RuDateAndMoneyConverter.CurrencyToTxtRub(finish, true));
            sheet1.GetRow(54).GetCell(GetLetterNumber("a")).SetCellValue("Сумма прописью: " + RuDateAndMoneyConverter.CurrencyToTxtRub(finish, true));


            //Force excel to recalculate all the formula while open
            sheet1.ForceFormulaRecalculation = true;

            WriteToFileRubFOld();
        }

        //рублевые акты + квитанции
        public static void GenerateFActSpecialRub(int id, int ULocationID)
        {
            UniversalEntity ue = new UniversalEntity();
            string header = string.Empty;
            string actNumber = string.Empty;
            string actNumber2 = string.Empty;
            string footer = string.Empty;

            FAbonentDO faDO = new FAbonentDO();

            ue = faDO.RetrieveByOrderID(id);
            FAbonent fa = new FAbonent();
            if (ue.Count > 0)
            {
                fa = (FAbonent)ue[0];
                header += fa.Surname + " " + fa.FirstName + " " + fa.LastName + " \nадрес: " + fa.Address + "\nтел.: " + fa.Phone;
            }

            FOrder fo = new FOrder();
            FOrderDO foDO = new FOrderDO();
            FOrderDetailsDO fodDO = new FOrderDetailsDO();


            ue = foDO.RetrieveFOrderById(id);
            if (ue.Count > 0)
            {
                fo = (FOrder)ue[0];
                actNumber = "  г. Донецк            АКТ № " + fo.Prefix + fo.ID + " за _________________________________ г.";
                actNumber2 = "                             по договору (письму) №  " + fo.Prefix + fo.ID + " от " + RuDateAndMoneyConverter.DateToTextLong(fo.DateIn) + " г.";
            }

            ue = faDO.RetrieveByOrderID(id);
            if (ue.Count > 0)
            {
                fa = (FAbonent) ue[0];
                footer += " Исполнитель:__________________Ломанов В.И. " + "                                           Заказчик:_______________" + fa.Surname + " " + fa.FirstName + " " + fa.LastName;
            }

            ue = fodDO.RetrieveFOrderDetailsByOrderID(id);

            InitializeWorkBookFActSpecialRub(); //InitializeWorkbook();
            ISheet sheet1 = hssfworkbook.GetSheet("s");
            //create cell on rows, since rows do already exist,it's not necessary to create rows again.
            sheet1.GetRow(2).GetCell(3).SetCellValue(header);
            sheet1.GetRow(32).GetCell(3).SetCellValue(header);
            sheet1.GetRow(9).GetCell(0).SetCellValue(actNumber);
            sheet1.GetRow(39).GetCell(0).SetCellValue(actNumber);
            sheet1.GetRow(11).GetCell(0).SetCellValue(actNumber2);
            sheet1.GetRow(26).GetCell(0).SetCellValue(footer);
            //sheet1.GetRow(41).GetCell(0).SetCellValue(actNumber2);

            /*int rowC = 44;
            double sum = 0;




            
            if (ue.Count > 0)
            {
                for (int i = row; i < ue.Count + row; i++)
                {
                    UOrderDetails u = (UOrderDetails)ue[i - row];

                    sheet1.GetRow(i).GetCell(GetLetterNumber("a")).SetCellValue("Определения метрологических характеристик водомера ");
                    sheet1.GetRow(i).GetCell(GetLetterNumber("ad")).SetCellValue("шт.");
                    sheet1.GetRow(i).GetCell(GetLetterNumber("aj")).SetCellValue("1");
                    sheet1.GetRow(i).GetCell(GetLetterNumber("ap")).SetCellValue(u.Price.ToString("0.00"));
                    sheet1.GetRow(i).GetCell(GetLetterNumber("av")).SetCellValue(u.Price.ToString("0.00"));
                    sum += u.Price;
                }
            }*/

            int rowC = 44;
            int row = 14;
            double sum = 0;
            double sumrub = 0;
            double getvat = 0;
            double finish = 0;
            int count = ue.Count;


            if (count <= 5)
            {
                CustomRetrieverDO crDO = new CustomRetrieverDO();
                ue = crDO.RetrieveFActByOrderID5LowRub(id);
                if (ue.Count > 0)
                {
                    for (int i = row; i < ue.Count + row; i++)
                    {
                        ArrayList al = (ArrayList)ue[i - row];
                        //FOrderDetailsAct u = (FOrderDetailsAct)fodal[i - row];

                        double price = 0;
                        double nds = 0;
                        double givememoney = 0;

                        price += Convert.ToDouble(al[3]);
                        nds = price * 0.2;
                        nds = Math.Round(nds, 2);
                        givememoney = (price + nds) + (price + nds);


                        sheet1.GetRow(i).GetCell(0).SetCellValue(i - row + 1);
                        sheet1.GetRow(i).GetCell(1).SetCellValue(string.Format("Определения метрологических характеристик водомера  D{0}", al[0].ToString()));
                        sheet1.GetRow(i).GetCell(2).SetCellValue("калькуляция");
                        sheet1.GetRow(i).GetCell(3).SetCellValue("шт.");
                        sheet1.GetRow(i).GetCell(4).SetCellValue(al[4].ToString());
                        sheet1.GetRow(i).GetCell(5).SetCellValue(Convert.ToDouble(al[3]).ToString("0.00"));
                        sheet1.GetRow(i).GetCell(6).SetCellValue(Convert.ToDouble(al[3]).ToString("0.00"));
                        //sheet1.GetRow(i).GetCell(5).SetCellValue(givememoney.ToString("0.00"));
                        //sheet1.GetRow(i).GetCell(6).SetCellValue(givememoney.ToString("0.00"));


                        sum += Convert.ToDouble(al[3]);

                        rowC++;

                    }
                }
            }
            else
            {
                CustomRetrieverDO crDO = new CustomRetrieverDO();
                ue = crDO.RetrieveFActByOrderID5HighRub(id);
                if (ue.Count > 0)
                {
                    for (int i = row; i < ue.Count + row; i++)
                    {
                        ArrayList al = (ArrayList)ue[i - row];
                        //FOrderDetailsAct u = (FOrderDetailsAct)fodal[i - row];

                        double littlebusiness = 0;
                        double makelittlebusiness = 0;
                        double givememoney = 0;

                        littlebusiness += Convert.ToDouble(al[1]);
                        makelittlebusiness = littlebusiness*0.2;
                        makelittlebusiness = Math.Round(makelittlebusiness, 2);
                        givememoney = (littlebusiness + makelittlebusiness) + (littlebusiness + makelittlebusiness);
                        
                        double business = 0;
                        double makebusiness = 0;
                        double somemoney = 0;

                        business += Convert.ToDouble(al[2]);
                        makebusiness = business*0.2;
                        makebusiness = Math.Round(makebusiness, 2);
                        somemoney = (business + makebusiness) + (business + makebusiness);



                        sheet1.GetRow(i).GetCell(0).SetCellValue(i - row + 1);
                        sheet1.GetRow(i).GetCell(1).SetCellValue(string.Format("Определения метрологических характеристик водомера  D{0}", al[0].ToString()));
                        sheet1.GetRow(i).GetCell(2).SetCellValue("калькуляция");
                        sheet1.GetRow(i).GetCell(3).SetCellValue("шт.");

                        sheet1.GetRow(i).GetCell(4).SetCellValue(al[3].ToString());
                        sheet1.GetRow(i).GetCell(5).SetCellValue(Convert.ToDouble(al[1]).ToString("0.00"));
                        sheet1.GetRow(i).GetCell(6).SetCellValue(Convert.ToDouble(al[2]).ToString("0.00"));

                        sum += Convert.ToDouble(al[2]);
                      //  sum += Convert.ToDouble(al[1]);
                        rowC++;
                    }
                }
            }
            //sheet1.GetRow(19).GetCell(6).SetCellValue(sum.ToString("0.00"));
            //sheet1.GetRow(20).GetCell(6).SetCellValue((sum / 100 * 20).ToString("0.00"));
            sheet1.GetRow(21).GetCell(6).SetCellValue(sum.ToString("0.00"));

            //sheet1.GetRow(24).GetCell(GetLetterNumber("a")).SetCellValue("Общая сумма, подлежащая оплате: " + RuDateAndMoneyConverter.CurrencyToTxtRub(finish, true));

           //рублевый счет
            sumrub = sum;
            getvat = sumrub*0.2;
            getvat = Math.Round(getvat, 2);
            finish = (sumrub + getvat) + (sumrub + getvat);
            //sheet1.GetRow(20).GetCell(6).SetCellValue((sum / 100 * 20).ToString("0.00"));
            sheet1.GetRow(21).GetCell(6).SetCellValue(sumrub.ToString("0.00"));

            sheet1.GetRow(24).GetCell(GetLetterNumber("a")).SetCellValue("Сумма прописью: " + RuDateAndMoneyConverter.CurrencyToTxtRub(sumrub, true));

            string tmp = string.Empty;
            tmp = sheet1.GetRow(29).GetCell(GetLetterNumber("c")).StringCellValue;

            string tmp1 = string.Empty;
            tmp1 = sheet1.GetRow(41).GetCell(GetLetterNumber("a")).StringCellValue;

            CustomRetrieverDO crdo = new CustomRetrieverDO();
            ue = crdo.RetrieveUserLocation(ULocationID);
            if (ue.Count > 0)
            {
                tmp1 = tmp1.Replace("257-42-61", (ue[0] as ArrayList)[2].ToString());
            }

            //tmp1 = tmp1.Replace("257-42-61", "");

            Vodomer vod = new Vodomer();
            
            tmp = tmp.Replace("FIO", fa.Surname + " " + fa.FirstName + " " + fa.LastName);
            tmp = tmp.Replace("ADDRESS", fa.Address);
            tmp = tmp.Replace("PNONE", fa.Phone);
            tmp = tmp.Replace("DATE", DateTime.Now.ToString("dd MMMM yyyy"));
            tmp = tmp.Replace("NUMBER", fo.Prefix + fo.ID.ToString());
            tmp = tmp.Replace("TYPE", fo.ActionType);
            tmp = tmp.Replace("NOMZAVOD", vod.FactoryNumber + " ");
            //tmp = tmp.Replace("SUM", sum.ToString("0.00"));
            //tmp = tmp.Replace("VAT", Utilities.GetVAT(sum).ToString("0.00"));
           // tmp = tmp.Replace("VSEGO", (sum + Utilities.GetVAT(sum)).ToString("0.00"));
            tmp = tmp.Replace("ALL", sumrub.ToString("0.00"));



            sheet1.GetRow(41).GetCell(GetLetterNumber("a")).SetCellValue(tmp1);
            sheet1.GetRow(29).GetCell(GetLetterNumber("c")).SetCellValue(tmp);
            sheet1.GetRow(41).GetCell(GetLetterNumber("c")).SetCellValue(tmp);

            /*sheet1.GetRow(49).GetCell(6).SetCellValue(sum.ToString("0.00"));
            sheet1.GetRow(50).GetCell(6).SetCellValue((sum / 100 * 20).ToString("0.00"));
            sheet1.GetRow(51).GetCell(6).SetCellValue((sum + (sum / 100 * 20)).ToString("0.00"));*/

            //Force excel to recalculate all the formula while open
            sheet1.ForceFormulaRecalculation = true;

            WriteToFileFSpecialRub();
        }


        public static void GenerateFActSpecialOld(int id, int ULocationID)
        {
            UniversalEntity ue = new UniversalEntity();
            string header = string.Empty;
            string actNumber = string.Empty;
            string actNumber2 = string.Empty;
            string footer = string.Empty;

            FAbonentDO faDO = new FAbonentDO();

            ue = faDO.RetrieveByOrderID(id);
            FAbonent fa = new FAbonent();
            if (ue.Count > 0)
            {
                fa = (FAbonent)ue[0];
                header += fa.Surname + " " + fa.FirstName + " " + fa.LastName + " \nадрес: " + fa.Address + "\nтел.: " + fa.Phone;
            }

            FOrder fo = new FOrder();
            FOrderDO foDO = new FOrderDO();
            FOrderDetailsDO fodDO = new FOrderDetailsDO();


            ue = foDO.RetrieveFOrderById(id);
            if (ue.Count > 0)
            {
                fo = (FOrder)ue[0];
                actNumber = "  г. Донецк            АКТ № " + fo.Prefix + fo.ID + " за _________________________________ г.";
                actNumber2 = "                             по договору (письму) №  " + fo.Prefix + fo.ID + " от " + RuDateAndMoneyConverter.DateToTextLong(fo.DateIn) + " г.";
            }

            ue = faDO.RetrieveByOrderID(id);
            if (ue.Count > 0)
            {
                fa = (FAbonent)ue[0];
                footer += " Исполнитель:__________________Ломанов В.И. " + "                                           Заказчик:_______________" + fa.Surname + " " + fa.FirstName + " " + fa.LastName;
            }

            ue = fodDO.RetrieveFOrderDetailsByOrderID(id);

            InitializeWorkBookFActSpecialOld(); //InitializeWorkbook();
            ISheet sheet1 = hssfworkbook.GetSheet("s");
            //create cell on rows, since rows do already exist,it's not necessary to create rows again.
            sheet1.GetRow(2).GetCell(3).SetCellValue(header);
            sheet1.GetRow(32).GetCell(3).SetCellValue(header);
            sheet1.GetRow(9).GetCell(0).SetCellValue(actNumber);
            sheet1.GetRow(39).GetCell(0).SetCellValue(actNumber);
            sheet1.GetRow(11).GetCell(0).SetCellValue(actNumber2);
            sheet1.GetRow(26).GetCell(0).SetCellValue(footer);
            //sheet1.GetRow(41).GetCell(0).SetCellValue(actNumber2);

            /*int rowC = 44;
            double sum = 0;




            
            if (ue.Count > 0)
            {
                for (int i = row; i < ue.Count + row; i++)
                {
                    UOrderDetails u = (UOrderDetails)ue[i - row];

                    sheet1.GetRow(i).GetCell(GetLetterNumber("a")).SetCellValue("Определения метрологических характеристик водомера ");
                    sheet1.GetRow(i).GetCell(GetLetterNumber("ad")).SetCellValue("шт.");
                    sheet1.GetRow(i).GetCell(GetLetterNumber("aj")).SetCellValue("1");
                    sheet1.GetRow(i).GetCell(GetLetterNumber("ap")).SetCellValue(u.Price.ToString("0.00"));
                    sheet1.GetRow(i).GetCell(GetLetterNumber("av")).SetCellValue(u.Price.ToString("0.00"));
                    sum += u.Price;
                }
            }*/

            int rowC = 44;
            int row = 14;
            double sum = 0;
            double sumrub = 0;
            double getvat = 0;
            double finish = 0;
            int count = ue.Count;


            if (count <= 5)
            {
                CustomRetrieverDO crDO = new CustomRetrieverDO();
                ue = crDO.RetrieveFActByOrderID5Low(id);
                if (ue.Count > 0)
                {
                    for (int i = row; i < ue.Count + row; i++)
                    {
                        ArrayList al = (ArrayList)ue[i - row];
                        //FOrderDetailsAct u = (FOrderDetailsAct)fodal[i - row];

                        double price = 0;
                        double nds = 0;
                        double givememoney = 0;

                        price += Convert.ToDouble(al[3]);
                        nds = price * 0.2;
                        nds = Math.Round(nds, 2);
                        givememoney = (price + nds) + (price + nds);


                        sheet1.GetRow(i).GetCell(0).SetCellValue(i - row + 1);
                        sheet1.GetRow(i).GetCell(1).SetCellValue(string.Format("Определения метрологических характеристик водомера  D{0}", al[0].ToString()));
                        sheet1.GetRow(i).GetCell(2).SetCellValue("калькуляция");
                        sheet1.GetRow(i).GetCell(3).SetCellValue("шт.");
                        sheet1.GetRow(i).GetCell(4).SetCellValue(al[4].ToString());
                        //sheet1.GetRow(i).GetCell(7).SetCellValue(Convert.ToDouble(al[3]).ToString("0.00"));
                        //sheet1.GetRow(i).GetCell(8).SetCellValue(Convert.ToDouble(al[3]).ToString("0.00"));
                        sheet1.GetRow(i).GetCell(5).SetCellValue(givememoney.ToString("0.00"));
                        sheet1.GetRow(i).GetCell(6).SetCellValue(givememoney.ToString("0.00"));


                        sum += Convert.ToDouble(al[3]);

                        rowC++;

                    }
                }
            }
            else
            {
                CustomRetrieverDO crDO = new CustomRetrieverDO();
                ue = crDO.RetrieveFActByOrderID5High(id);
                if (ue.Count > 0)
                {
                    for (int i = row; i < ue.Count + row; i++)
                    {
                        ArrayList al = (ArrayList)ue[i - row];
                        //FOrderDetailsAct u = (FOrderDetailsAct)fodal[i - row];

                        double littlebusiness = 0;
                        double makelittlebusiness = 0;
                        double givememoney = 0;

                        littlebusiness += Convert.ToDouble(al[1]);
                        makelittlebusiness = littlebusiness * 0.2;
                        makelittlebusiness = Math.Round(makelittlebusiness, 2);
                        givememoney = (littlebusiness + makelittlebusiness) + (littlebusiness + makelittlebusiness);

                        double business = 0;
                        double makebusiness = 0;
                        double somemoney = 0;

                        business += Convert.ToDouble(al[2]);
                        makebusiness = business * 0.2;
                        makebusiness = Math.Round(makebusiness, 2);
                        somemoney = (business + makebusiness) + (business + makebusiness);



                        sheet1.GetRow(i).GetCell(0).SetCellValue(i - row + 1);
                        sheet1.GetRow(i).GetCell(1).SetCellValue(string.Format("Определения метрологических характеристик водомера  D{0}", al[0].ToString()));
                        sheet1.GetRow(i).GetCell(2).SetCellValue("калькуляция");
                        sheet1.GetRow(i).GetCell(3).SetCellValue("шт.");

                        sheet1.GetRow(i).GetCell(4).SetCellValue(al[3].ToString());
                        sheet1.GetRow(i).GetCell(5).SetCellValue(givememoney.ToString("0.00"));
                        sheet1.GetRow(i).GetCell(6).SetCellValue(somemoney.ToString("0.00"));

                        sum += Convert.ToDouble(al[2]);
                        //  sum += Convert.ToDouble(al[1]);
                        rowC++;
                    }
                }
            }
            //sheet1.GetRow(19).GetCell(6).SetCellValue(sum.ToString("0.00"));
            //sheet1.GetRow(20).GetCell(6).SetCellValue((sum / 100 * 20).ToString("0.00"));
            sheet1.GetRow(21).GetCell(6).SetCellValue(sum.ToString("0.00"));

            //sheet1.GetRow(24).GetCell(GetLetterNumber("a")).SetCellValue("Общая сумма, подлежащая оплате: " + RuDateAndMoneyConverter.CurrencyToTxtRub(finish, true));

            //рублевый счет
            sumrub = sum;
            getvat = sumrub * 0.2;
            getvat = Math.Round(getvat, 2);
            finish = (sumrub + getvat) + (sumrub + getvat);
            //sheet1.GetRow(20).GetCell(6).SetCellValue((sum / 100 * 20).ToString("0.00"));
            sheet1.GetRow(21).GetCell(6).SetCellValue(finish.ToString("0.00"));

            sheet1.GetRow(24).GetCell(GetLetterNumber("a")).SetCellValue("Сумма прописью: " + RuDateAndMoneyConverter.CurrencyToTxtRub(finish, true));

            string tmp = string.Empty;
            tmp = sheet1.GetRow(29).GetCell(GetLetterNumber("c")).StringCellValue;

            string tmp1 = string.Empty;
            tmp1 = sheet1.GetRow(41).GetCell(GetLetterNumber("a")).StringCellValue;

            CustomRetrieverDO crdo = new CustomRetrieverDO();
            ue = crdo.RetrieveUserLocation(ULocationID);
            if (ue.Count > 0)
            {
                tmp1 = tmp1.Replace("257-42-61", (ue[0] as ArrayList)[2].ToString());
            }

            //tmp1 = tmp1.Replace("257-42-61", "");

            Vodomer vod = new Vodomer();

            tmp = tmp.Replace("FIO", fa.Surname + " " + fa.FirstName + " " + fa.LastName);
            tmp = tmp.Replace("ADDRESS", fa.Address);
            tmp = tmp.Replace("PNONE", fa.Phone);
            tmp = tmp.Replace("DATE", DateTime.Now.ToString("dd MMMM yyyy"));
            tmp = tmp.Replace("NUMBER", fo.Prefix + fo.ID.ToString());
            tmp = tmp.Replace("TYPE", fo.ActionType);
            tmp = tmp.Replace("NOMZAVOD", vod.FactoryNumber + " ");
            //tmp = tmp.Replace("SUM", sum.ToString("0.00"));
            //tmp = tmp.Replace("VAT", Utilities.GetVAT(sum).ToString("0.00"));
            // tmp = tmp.Replace("VSEGO", (sum + Utilities.GetVAT(sum)).ToString("0.00"));
            tmp = tmp.Replace("ALL", finish.ToString("0.00"));



            sheet1.GetRow(41).GetCell(GetLetterNumber("a")).SetCellValue(tmp1);
            sheet1.GetRow(29).GetCell(GetLetterNumber("c")).SetCellValue(tmp);
            sheet1.GetRow(41).GetCell(GetLetterNumber("c")).SetCellValue(tmp);

            /*sheet1.GetRow(49).GetCell(6).SetCellValue(sum.ToString("0.00"));
            sheet1.GetRow(50).GetCell(6).SetCellValue((sum / 100 * 20).ToString("0.00"));
            sheet1.GetRow(51).GetCell(6).SetCellValue((sum + (sum / 100 * 20)).ToString("0.00"));*/

            //Force excel to recalculate all the formula while open
            sheet1.ForceFormulaRecalculation = true;

            WriteToFileFSpeciaOld();
        }

        //генерация рублевого счета фактуры
        public static void GenerateBillRub(int id)
        {
            InitializeWorkbookBillRub();

            ISheet sheet1 = hssfworkbook.GetSheet("s");


            UniversalEntity ue = new UniversalEntity();
            //UniversalEntity uev = new UniversalEntity();

            UAbonent ua = new UAbonent();
            UAbonentDO uado = new UAbonentDO();
            UOrder uo = new UOrder();
            UOrderDO uodo = new UOrderDO();
            UOrderDetailsDO uodDO = new UOrderDetailsDO();

            ue = uado.RetrieveByOrderID(id);
            string header = string.Empty;
            string actNumber = string.Empty;
            if (ue.Count > 0)
            {
                ua = (UAbonent)ue[0];
                header += ua.Title + " \nюр адрес: " + ua.Address +  " \n№ ИНН: " + ua.INN + " \nтел.: " + ua.Phone;
            }
            ue = uodo.RetrieveUOrderById(id);
            if (ue.Count > 0)
            {
                uo = (UOrder)ue[0];
                sheet1.GetRow(4).GetCell(GetLetterNumber("aj")).SetCellValue(uo.ID);
                sheet1.GetRow(9).GetCell(GetLetterNumber("ai")).SetCellValue(RuDateAndMoneyConverter.DateToTextLong(uo.DateIn) + "г.");


            }
            ue = uodDO.RetrieveUOrderDetailsByOrderID(id);
            sheet1.GetRow(16).GetCell(6).SetCellValue(header);



            int count = ue.Count;
            double sum = 0;
            double sumrub = 0;
            double getvat = 0;
            double finish = 0;

            int row = 20;
            if (count <= 5)
            {
                CustomRetrieverDO crDO = new CustomRetrieverDO();
                ue = crDO.RetrieveUActByOrderID5Low(id);
                if (ue.Count > 0)
                {
                    for (int i = row; i < ue.Count + row; i++)
                    {
                        ArrayList al = (ArrayList)ue[i - row];

                        double sumone = 0;
                        double getnds = 0;
                        //double getsum = 0;
                        double getfin = 0;

                        sumone += Convert.ToDouble(al[3]);
                        getnds = sumone*0.2;
                        getnds = Math.Round(getnds, 2);
                        getfin = (getnds + sumone) + (getnds + sumone);
                       
                        sheet1.GetRow(i).GetCell(GetLetterNumber("a")).SetCellValue(string.Format("Определения метрологических характеристик водомера  D{0}", al[0].ToString()));
                        sheet1.GetRow(i).GetCell(GetLetterNumber("ad")).SetCellValue("шт.");
                        sheet1.GetRow(i).GetCell(GetLetterNumber("aj")).SetCellValue(al[4].ToString());
                        sheet1.GetRow(i).GetCell(GetLetterNumber("ap")).SetCellValue(getfin.ToString("0.00"));
                        sheet1.GetRow(i).GetCell(GetLetterNumber("av")).SetCellValue(getfin.ToString("0.00"));
                       /* sheet1.GetRow(i).GetCell(GetLetterNumber("ap")).SetCellValue(Convert.ToDouble(al[3]));
                        sheet1.GetRow(i).GetCell(GetLetterNumber("av")).SetCellValue(Convert.ToDouble(al[3]));*/
                        sum += Convert.ToDouble(al[3]);
                       
                        
                    }
                }
            }
            else
            {
                CustomRetrieverDO crDO = new CustomRetrieverDO();
                ue = crDO.RetrieveUActByOrderID5High(id);
                if (ue.Count > 0)
                {
                    for (int i = row; i < ue.Count + row; i++)
                    {
                        ArrayList al = (ArrayList)ue[i - row];
                        //FOrderDetailsAct u = (FOrderDetailsAct)fodal[i - row];

                        double sumone = 0;
                        double getnds = 0;
                        //double getsum = 0;
                        double getfin = 0;

                        sumone += Convert.ToDouble(al[1]);
                        getnds = sumone * 0.2;
                        getnds = Math.Round(getnds, 2);
                        getfin = (getnds + sumone) + (getnds + sumone);

                        double forone = 0;
                        double ndsforone = 0;
                        double finalforone = 0;

                        forone += Convert.ToDouble(al[2]);
                        ndsforone = forone*0.2;
                        ndsforone = Math.Round(ndsforone, 2);
                        finalforone = (forone + ndsforone) + (forone + ndsforone);

                        sheet1.GetRow(i).GetCell(GetLetterNumber("a")).SetCellValue(string.Format("Определения метрологических характеристик водомера  D{0}", al[0].ToString()));
                        sheet1.GetRow(i).GetCell(GetLetterNumber("ad")).SetCellValue("шт.");
                        sheet1.GetRow(i).GetCell(GetLetterNumber("aj")).SetCellValue(al[3].ToString());
                        //sheet1.GetRow(i).GetCell(GetLetterNumber("av")).SetCellValue(getfin.ToString("0.00"));
                        sheet1.GetRow(i).GetCell(GetLetterNumber("ap")).SetCellValue(getfin.ToString("0.00"));
                        sheet1.GetRow(i).GetCell(GetLetterNumber("av")).SetCellValue(finalforone.ToString("0.00"));

                        sum += Convert.ToDouble(al[2]);
                    }
                }
            }

            sumrub = sum;
            getvat = sumrub*0.2;
            getvat = Math.Round(getvat, 2);
            finish = (sumrub + getvat) + (sumrub + getvat);

            /*int row = 20;
            double sum = 0;




            
            if (ue.Count > 0)
            {
                for (int i = row; i < ue.Count + row; i++)
                {
                    UOrderDetails u = (UOrderDetails)ue[i - row];

                    sheet1.GetRow(i).GetCell(GetLetterNumber("a")).SetCellValue("Определения метрологических характеристик водомера ");
                    sheet1.GetRow(i).GetCell(GetLetterNumber("ad")).SetCellValue("шт.");
                    sheet1.GetRow(i).GetCell(GetLetterNumber("aj")).SetCellValue("1");
                    sheet1.GetRow(i).GetCell(GetLetterNumber("ap")).SetCellValue(u.Price.ToString("0.00"));
                    sheet1.GetRow(i).GetCell(GetLetterNumber("av")).SetCellValue(u.Price.ToString("0.00"));
                    sum += u.Price;
                }
            }*/

            // sheet1.GetRow(30).GetCell(GetLetterNumber("av")).SetCellValue(sum.ToString("0.00"));
          //  sheet1.GetRow(31).GetCell(GetLetterNumber("av")).SetCellValue(Utilities.GetVAT(sum).ToString("0.00"));
            sheet1.GetRow(31).GetCell(GetLetterNumber("av")).SetCellValue(finish.ToString("0.00"));
            //sumrub = (sum)*2;
            //sheet1.GetRow(68).GetCell(GetLetterNumber("av")).SetCellValue(((sumrub)*2).ToString("0.00"));
            sheet1.GetRow(32).GetCell(GetLetterNumber("av")).SetCellValue(finish.ToString("0.00"));
            
            sheet1.GetRow(35).GetCell(GetLetterNumber("a")).SetCellValue("Общая сумма, подлежащая оплате: " + RuDateAndMoneyConverter.CurrencyToTxt(finish, true));

            
            //Загальна сума, що підлягає оплаті


            //Force excel to recalculate all the formula while open
            sheet1.ForceFormulaRecalculation = true;

            WriteToFileBRub();
        }


        //генрация рублевого акта для юр лиц
        public static void GenerateUactRub(int id)
        {
            UniversalEntity ue = new UniversalEntity();
            UAbonent ua = new UAbonent();
            UAbonentDO uado = new UAbonentDO();
            UOrder uo = new UOrder();
            UOrderDO uodo = new UOrderDO();
            UOrderDetailsDO uodDO = new UOrderDetailsDO();

            ue = uado.RetrieveByOrderID(id);
            string header = string.Empty;
            string actNumber = string.Empty;
            if (ue.Count > 0)
            {
                ua = (UAbonent)ue[0];
                header += ua.Title + " \nюр адрес: " + ua.Address + " \n№ ИНН: " + ua.INN + " \nтел.: " + ua.Phone;
            }
            ue = uodo.RetrieveUOrderById(id);
            if (ue.Count > 0)
            {
                uo = (UOrder)ue[0];
                if (uo.DateOut.HasValue)
                {
                    actNumber = "  г. Донецк            АКТ № " + uo.ID + " за _________________________________г.";
                }
                else
                {
                    actNumber = "  г. Донецк            АКТ № " + uo.ID + " за _________________________________ г.";
                }
            }

            ue = uodDO.RetrieveUOrderDetailsByOrderID(id);

            InitializeWorkBookUActRub();

            ISheet sheet1 = hssfworkbook.GetSheet("s");

            sheet1.GetRow(11).GetCell(0).SetCellValue("                             по договору (письму) № " + uo.ID + " от " + RuDateAndMoneyConverter.DateToTextLong(uo.DateIn) + " г.");

            sheet1.GetRow(41).GetCell(0).SetCellValue("                             по договору (письму) № " + uo.ID + " от " + RuDateAndMoneyConverter.DateToTextLong(uo.DateIn) + " г.");

            //create cell on rows, since rows do already exist,it's not necessary to create rows again.
            sheet1.GetRow(2).GetCell(3).SetCellValue(header);
            sheet1.GetRow(32).GetCell(3).SetCellValue(header);
            sheet1.GetRow(9).GetCell(0).SetCellValue(actNumber);
            sheet1.GetRow(39).GetCell(0).SetCellValue(actNumber);

            int rowC = 44;
            int row = 14;
            double sum = 0;
            //double vat = 0;
            //double result = 0;
            int count = ue.Count;

            if (count <= 5)
            {
                CustomRetrieverDO crDO = new CustomRetrieverDO();
                ue = crDO.RetrieveUActByOrderID5Low(id);
                if (ue.Count > 0)
                {
                    //uo = (UOrder)ue[0];
                    //ue = uodDO.RetrieveUOrderDetailsByOrderID(id);
                   /* foreach (UOrderDetails uod in ue)
                    {
                        sum += uod.Price;
                    }*/
                    for (int i = row; i < ue.Count + row; i++)
                    {
                        ArrayList al = (ArrayList)ue[i - row];

                        //FOrderDetailsAct u = (FOrderDetailsAct)fodal[i - row];

                        double sumone = 0;
                        double getnds = 0;
                        double getfin = 0;

                        sumone += Convert.ToDouble(al[3]);
                        getnds = sumone*0.2;
                        getnds = Math.Round(getnds, 2);
                        getfin = (getnds + sumone) + (getnds + sumone);

                        sheet1.GetRow(i).GetCell(0).SetCellValue(i - row + 1);
                        sheet1.GetRow(i).GetCell(1).SetCellValue(string.Format("Определения метрологических характеристик водомера D{0}", al[0].ToString()));
                        sheet1.GetRow(i).GetCell(2).SetCellValue("калькуляция");
                        sheet1.GetRow(i).GetCell(3).SetCellValue("шт.");
                        sheet1.GetRow(i).GetCell(4).SetCellValue(al[4].ToString());
                       // sheet1.GetRow(i).GetCell(7).SetCellValue(Convert.ToDouble(al[3]).ToString("0.00"));
                       // sheet1.GetRow(i).GetCell(8).SetCellValue(Convert.ToDouble(al[3]).ToString("0.00"));
                        sheet1.GetRow(i).GetCell(5).SetCellValue(getfin.ToString("0.00"));
                        sheet1.GetRow(i).GetCell(6).SetCellValue(getfin.ToString("0.00"));

                        sheet1.GetRow(rowC).GetCell(0).SetCellValue(i - row + 1);
                        sheet1.GetRow(rowC).GetCell(1).SetCellValue(string.Format("Определения метрологических характеристик водомера  D{0}", al[0].ToString()));
                        sheet1.GetRow(rowC).GetCell(2).SetCellValue("калькуляция");
                        sheet1.GetRow(rowC).GetCell(3).SetCellValue("шт.");
                        sheet1.GetRow(rowC).GetCell(4).SetCellValue(al[4].ToString());
                      //  sheet1.GetRow(rowC).GetCell(7).SetCellValue(Convert.ToDouble(al[3]).ToString("0.00"));
                      //  sheet1.GetRow(rowC).GetCell(8).SetCellValue(Convert.ToDouble(al[3]).ToString("0.00"));
                        sheet1.GetRow(rowC).GetCell(5).SetCellValue(getfin.ToString("0.00"));
                        sheet1.GetRow(rowC).GetCell(6).SetCellValue(getfin.ToString("0.00"));

                        sum += Convert.ToDouble(al[3]);
                       
                        rowC++;
                    }
                }
            }
            else
            {
                CustomRetrieverDO crDO = new CustomRetrieverDO();
                ue = crDO.RetrieveUActByOrderID5High(id);
                if (ue.Count > 0)
                {
                    for (int i = row; i < ue.Count + row; i++)
                    {
                        ArrayList al = (ArrayList)ue[i - row];
                        //FOrderDetailsAct u = (FOrderDetailsAct)fodal[i - row];

                        double sumone = 0;
                        double sumnds = 0;
                        double sumfinish = 0;

                        sumone += Convert.ToDouble(al[1]);
                        sumnds = sumone*0.2;
                        sumnds = Math.Round(sumnds, 2);
                        sumfinish = (sumone + sumnds) + (sumone + sumnds);


                        double sumtwo = 0;
                        double twonds = 0;
                        double twofinish = 0;

                        sumtwo += Convert.ToDouble(al[2]);
                        twonds = sumtwo*0.2;
                        twonds = Math.Round(twonds, 2);
                        twofinish = (twonds + sumtwo) + (sumtwo + twonds);


                        sheet1.GetRow(i).GetCell(0).SetCellValue(i - row + 1);
                        sheet1.GetRow(i).GetCell(1).SetCellValue(string.Format("Определения метрологических характеристик водомера  D{0}", al[0].ToString()));
                        sheet1.GetRow(i).GetCell(2).SetCellValue("калькуляция");
                        sheet1.GetRow(i).GetCell(3).SetCellValue("шт.");
                        sheet1.GetRow(i).GetCell(4).SetCellValue(al[3].ToString());
                        sheet1.GetRow(i).GetCell(5).SetCellValue(sumfinish.ToString("0.00"));
                        sheet1.GetRow(i).GetCell(6).SetCellValue(twofinish.ToString("0.00"));


                        sheet1.GetRow(rowC).GetCell(0).SetCellValue(i - row + 1);
                        sheet1.GetRow(rowC).GetCell(1).SetCellValue(string.Format("Определения метрологических характеристик водомера  D{0}", al[0].ToString()));
                        sheet1.GetRow(rowC).GetCell(2).SetCellValue("калькуляция");
                        sheet1.GetRow(rowC).GetCell(3).SetCellValue("шт.");
                        sheet1.GetRow(rowC).GetCell(4).SetCellValue(al[3].ToString());
                        sheet1.GetRow(rowC).GetCell(5).SetCellValue(sumfinish.ToString("0.00"));
                        sheet1.GetRow(rowC).GetCell(6).SetCellValue(twofinish.ToString("0.00"));

                        sum += Convert.ToDouble(al[2]);
                        
                        rowC++;
                    }
                }
            }






            /*int rowC = 44;
            int row = 14;
            double sum = 0;
            if (ue.Count > 0)
            {
                for (int i = row; i < ue.Count + row; i++)
                {
                    UOrderDetails u = (UOrderDetails)ue[i - row];

                    sheet1.GetRow(i).GetCell(0).SetCellValue(i - row+1);
                    sheet1.GetRow(i).GetCell(1).SetCellValue(string.Format("Определения метрологических характеристик водомера  D{0}, {1}, {2}", al[0].ToString(), al[1].ToString(), al[2].ToString()));
                    sheet1.GetRow(i).GetCell(2).SetCellValue("калькуляция");
                    sheet1.GetRow(i).GetCell(3).SetCellValue("шт.");
                    sheet1.GetRow(i).GetCell(4).SetCellValue("1");
                    sheet1.GetRow(i).GetCell(5).SetCellValue(u.Price.ToString("0.00"));
                    sheet1.GetRow(i).GetCell(6).SetCellValue(u.Price.ToString("0.00"));

                    sheet1.GetRow(rowC).GetCell(0).SetCellValue(i - row + 1);
                    sheet1.GetRow(rowC).GetCell(1).SetCellValue(string.Format("Определения метрологических характеристик водомера  D{0}, {1}, {2}", al[0].ToString(), al[1].ToString(), al[2].ToString()));
                    sheet1.GetRow(rowC).GetCell(2).SetCellValue("калькуляция");
                    sheet1.GetRow(rowC).GetCell(3).SetCellValue("шт.");
                    sheet1.GetRow(rowC).GetCell(4).SetCellValue("1");
                    sheet1.GetRow(rowC).GetCell(5).SetCellValue(u.Price.ToString("0.00"));
                    sheet1.GetRow(rowC).GetCell(6).SetCellValue(u.Price.ToString("0.00"));

                    sum += u.Price;
                    rowC++;
                }
            }*/
            double getvat = 0;
            double finish = 0;
            double sumrub = 0;

            sumrub = sum;
            getvat = sumrub * 0.2;
            getvat = Math.Round(getvat, 2);
            finish = (sumrub + getvat) + (sumrub + getvat);

           // sheet1.GetRow(19).GetCell(6).SetCellValue((sum + Utilities.GetVAT(sum)).ToString("0.00"));
           // sheet1.GetRow(20).GetCell(6).SetCellValue(Utilities.GetVATRub(sum).ToString("0.00"));
            
           // sheet1.GetRow(14).GetCell(6).SetCellValue((sum + Utilities.GetVAT(sum)).ToString("0.00"));
          //  sheet1.GetRow(15).GetCell(6).SetCellValue((sum + Utilities.GetVAT(sum)).ToString("0.00"));
          //  sheet1.GetRow(16).GetCell(6).SetCellValue((sum + Utilities.GetVAT(sum)).ToString("0.00"));
          //  sheet1.GetRow(17).GetCell(6).SetCellValue((sum + Utilities.GetVAT(sum)).ToString("0.00"));
           // sheet1.GetRow(18).GetCell(6).SetCellValue((sum + Utilities.GetVAT(sum)).ToString("0.00"));
            sheet1.GetRow(21).GetCell(6).SetCellValue(finish.ToString("0.00"));

           // sheet1.GetRow(49).GetCell(6).SetCellValue((sum + Utilities.GetVAT(sum)).ToString("0.00"));
           // sheet1.GetRow(50).GetCell(6).SetCellValue(Utilities.GetVATRub(sum).ToString("0.00"));
            
           // sheet1.GetRow(44).GetCell(6).SetCellValue((sum + Utilities.GetVAT(sum)).ToString("0.00"));
           // sheet1.GetRow(45).GetCell(6).SetCellValue((sum + Utilities.GetVAT(sum)).ToString("0.00"));
          //  sheet1.GetRow(46).GetCell(6).SetCellValue((sum + Utilities.GetVAT(sum)).ToString("0.00"));
          //  sheet1.GetRow(47).GetCell(6).SetCellValue((sum + Utilities.GetVAT(sum)).ToString("0.00"));
          //  sheet1.GetRow(48).GetCell(6).SetCellValue((sum + Utilities.GetVAT(sum)).ToString("0.00"));
            sheet1.GetRow(51).GetCell(6).SetCellValue(finish.ToString("0.00"));
            /* sheet1.GetRow(2).GetCell(1).SetCellValue(300);
             sheet1.GetRow(3).GetCell(1).SetCellValue(500050);
             sheet1.GetRow(4).GetCell(1).SetCellValue(8000);
             sheet1.GetRow(5).GetCell(1).SetCellValue(110);
             sheet1.GetRow(6).GetCell(1).SetCellValue(100);
             sheet1.GetRow(7).GetCell(1).SetCellValue(200);
             sheet1.GetRow(8).GetCell(1).SetCellValue(210);
             sheet1.GetRow(9).GetCell(1).SetCellValue(2300);
             sheet1.GetRow(10).GetCell(1).SetCellValue(240);
             sheet1.GetRow(11).GetCell(1).SetCellValue(180123);
             sheet1.GetRow(12).GetCell(1).SetCellValue(150);*/

            //Force excel to recalculate all the formula while open
            sheet1.ForceFormulaRecalculation = true;

            WriteToFileUARub();
        }

        //генрация рублевого акта для юр лиц(только рубли)
        public static void GenerateUactRubles(int id)
        {
            UniversalEntity ue = new UniversalEntity();
            UAbonent ua = new UAbonent();
            UAbonentDO uado = new UAbonentDO();
            UOrder uo = new UOrder();
            UOrderDO uodo = new UOrderDO();
            UOrderDetailsDO uodDO = new UOrderDetailsDO();

            ue = uado.RetrieveByOrderID(id);
            string header = string.Empty;
            string actNumber = string.Empty;
            if (ue.Count > 0)
            {
                ua = (UAbonent)ue[0];
                header += ua.Title + " \nюр адрес: " + ua.Address + " \n№ ИНН: " + ua.INN + " \nтел.: " + ua.Phone;
            }
            ue = uodo.RetrieveUOrderById(id);
            if (ue.Count > 0)
            {
                uo = (UOrder)ue[0];
                if (uo.DateOut.HasValue)
                {
                    actNumber = "  г. Донецк            АКТ № " + uo.ID + " за _________________________________г.";
                }
                else
                {
                    actNumber = "  г. Донецк            АКТ № " + uo.ID + " за _________________________________ г.";
                }
            }

            ue = uodDO.RetrieveUOrderDetailsByOrderID(id);

            InitializeWorkBookUActRubles();

            ISheet sheet1 = hssfworkbook.GetSheet("s");

            sheet1.GetRow(11).GetCell(0).SetCellValue("                             по договору (письму) № " + uo.ID + " от " + RuDateAndMoneyConverter.DateToTextLong(uo.DateIn) + " г.");

            sheet1.GetRow(41).GetCell(0).SetCellValue("                             по договору (письму) № " + uo.ID + " от " + RuDateAndMoneyConverter.DateToTextLong(uo.DateIn) + " г.");

            //create cell on rows, since rows do already exist,it's not necessary to create rows again.
            sheet1.GetRow(2).GetCell(3).SetCellValue(header);
            sheet1.GetRow(32).GetCell(3).SetCellValue(header);
            sheet1.GetRow(9).GetCell(0).SetCellValue(actNumber);
            sheet1.GetRow(39).GetCell(0).SetCellValue(actNumber);

            int rowC = 44;
            int row = 14;
            double sum = 0;
            //double vat = 0;
            //double result = 0;
            int count = ue.Count;

            if (count <= 5)
            {
                CustomRetrieverDO crDO = new CustomRetrieverDO();
                ue = crDO.RetrieveUActByOrderID5LowRub(id);
                if (ue.Count > 0)
                {
                    //uo = (UOrder)ue[0];
                    //ue = uodDO.RetrieveUOrderDetailsByOrderID(id);
                    /* foreach (UOrderDetails uod in ue)
                     {
                         sum += uod.Price;
                     }*/
                    for (int i = row; i < ue.Count + row; i++)
                    {
                        ArrayList al = (ArrayList)ue[i - row];

                        //FOrderDetailsAct u = (FOrderDetailsAct)fodal[i - row];

                        double sumone = 0;
                        double getnds = 0;
                        double getfin = 0;

                        sumone += Convert.ToDouble(al[3]);
                        getnds = sumone * 0.2;
                        getnds = Math.Round(getnds, 2);
                        getfin = (getnds + sumone) + (getnds + sumone);

                        sheet1.GetRow(i).GetCell(0).SetCellValue(i - row + 1);
                        sheet1.GetRow(i).GetCell(1).SetCellValue(string.Format("Определения метрологических характеристик водомера D{0}", al[0].ToString()));
                        sheet1.GetRow(i).GetCell(2).SetCellValue("калькуляция");
                        sheet1.GetRow(i).GetCell(3).SetCellValue("шт.");
                        sheet1.GetRow(i).GetCell(4).SetCellValue(al[4].ToString());
                        sheet1.GetRow(i).GetCell(5).SetCellValue(Convert.ToDouble(al[3]).ToString("0.00"));
                        sheet1.GetRow(i).GetCell(6).SetCellValue(Convert.ToDouble(al[3]).ToString("0.00"));
                        sheet1.GetRow(i).GetCell(7).SetCellValue(getfin.ToString("0.00"));
                        sheet1.GetRow(i).GetCell(8).SetCellValue(getfin.ToString("0.00"));

                        sheet1.GetRow(rowC).GetCell(0).SetCellValue(i - row + 1);
                        sheet1.GetRow(rowC).GetCell(1).SetCellValue(string.Format("Определения метрологических характеристик водомера  D{0}", al[0].ToString()));
                        sheet1.GetRow(rowC).GetCell(2).SetCellValue("калькуляция");
                        sheet1.GetRow(rowC).GetCell(3).SetCellValue("шт.");
                        sheet1.GetRow(rowC).GetCell(4).SetCellValue(al[4].ToString());
                        sheet1.GetRow(rowC).GetCell(5).SetCellValue(Convert.ToDouble(al[3]).ToString("0.00"));
                        sheet1.GetRow(rowC).GetCell(6).SetCellValue(Convert.ToDouble(al[3]).ToString("0.00"));
                        sheet1.GetRow(rowC).GetCell(7).SetCellValue(getfin.ToString("0.00"));
                        sheet1.GetRow(rowC).GetCell(8).SetCellValue(getfin.ToString("0.00"));

                        sum += Convert.ToDouble(al[3]);

                        rowC++;
                    }
                }
            }
            else
            {
                CustomRetrieverDO crDO = new CustomRetrieverDO();
                ue = crDO.RetrieveUActByOrderID5HighRub(id);
                if (ue.Count > 0)
                {
                    for (int i = row; i < ue.Count + row; i++)
                    {
                        ArrayList al = (ArrayList)ue[i - row];
                        //FOrderDetailsAct u = (FOrderDetailsAct)fodal[i - row];


                        sheet1.GetRow(i).GetCell(0).SetCellValue(i - row + 1);
                        sheet1.GetRow(i).GetCell(1).SetCellValue(string.Format("Определения метрологических характеристик водомера  D{0}", al[0].ToString()));
                        sheet1.GetRow(i).GetCell(2).SetCellValue("калькуляция");
                        sheet1.GetRow(i).GetCell(3).SetCellValue("шт.");
                        sheet1.GetRow(i).GetCell(4).SetCellValue(al[3].ToString());
                        sheet1.GetRow(i).GetCell(5).SetCellValue(Convert.ToDouble(al[1]).ToString("0.00"));
                        sheet1.GetRow(i).GetCell(6).SetCellValue(Convert.ToDouble(al[2]).ToString("0.00"));


                        sheet1.GetRow(rowC).GetCell(0).SetCellValue(i - row + 1);
                        sheet1.GetRow(rowC).GetCell(1).SetCellValue(string.Format("Определения метрологических характеристик водомера  D{0}", al[0].ToString()));
                        sheet1.GetRow(rowC).GetCell(2).SetCellValue("калькуляция");
                        sheet1.GetRow(rowC).GetCell(3).SetCellValue("шт.");
                        sheet1.GetRow(rowC).GetCell(4).SetCellValue(al[3].ToString());
                        sheet1.GetRow(rowC).GetCell(5).SetCellValue(Convert.ToDouble(al[1]).ToString("0.00"));
                        sheet1.GetRow(rowC).GetCell(6).SetCellValue(Convert.ToDouble(al[2]).ToString("0.00"));

                        sum += Convert.ToDouble(al[2]);

                        rowC++;
                    }
                }
            }






            /*int rowC = 44;
            int row = 14;
            double sum = 0;
            if (ue.Count > 0)
            {
                for (int i = row; i < ue.Count + row; i++)
                {
                    UOrderDetails u = (UOrderDetails)ue[i - row];

                    sheet1.GetRow(i).GetCell(0).SetCellValue(i - row+1);
                    sheet1.GetRow(i).GetCell(1).SetCellValue(string.Format("Определения метрологических характеристик водомера  D{0}, {1}, {2}", al[0].ToString(), al[1].ToString(), al[2].ToString()));
                    sheet1.GetRow(i).GetCell(2).SetCellValue("калькуляция");
                    sheet1.GetRow(i).GetCell(3).SetCellValue("шт.");
                    sheet1.GetRow(i).GetCell(4).SetCellValue("1");
                    sheet1.GetRow(i).GetCell(5).SetCellValue(u.Price.ToString("0.00"));
                    sheet1.GetRow(i).GetCell(6).SetCellValue(u.Price.ToString("0.00"));

                    sheet1.GetRow(rowC).GetCell(0).SetCellValue(i - row + 1);
                    sheet1.GetRow(rowC).GetCell(1).SetCellValue(string.Format("Определения метрологических характеристик водомера  D{0}, {1}, {2}", al[0].ToString(), al[1].ToString(), al[2].ToString()));
                    sheet1.GetRow(rowC).GetCell(2).SetCellValue("калькуляция");
                    sheet1.GetRow(rowC).GetCell(3).SetCellValue("шт.");
                    sheet1.GetRow(rowC).GetCell(4).SetCellValue("1");
                    sheet1.GetRow(rowC).GetCell(5).SetCellValue(u.Price.ToString("0.00"));
                    sheet1.GetRow(rowC).GetCell(6).SetCellValue(u.Price.ToString("0.00"));

                    sum += u.Price;
                    rowC++;
                }
            }*/
            double price = 0;
            double nds = 0;
            double finalsum = 0;

            price = sum;
            nds = price*0.2;
            nds = Math.Round(nds, 2);
            finalsum = (nds + price) + (price + nds);


            // sheet1.GetRow(19).GetCell(6).SetCellValue((sum + Utilities.GetVAT(sum)).ToString("0.00"));
            // sheet1.GetRow(20).GetCell(6).SetCellValue(Utilities.GetVATRub(sum).ToString("0.00"));

            // sheet1.GetRow(14).GetCell(6).SetCellValue((sum + Utilities.GetVAT(sum)).ToString("0.00"));
            //  sheet1.GetRow(15).GetCell(6).SetCellValue((sum + Utilities.GetVAT(sum)).ToString("0.00"));
            //  sheet1.GetRow(16).GetCell(6).SetCellValue((sum + Utilities.GetVAT(sum)).ToString("0.00"));
            //  sheet1.GetRow(17).GetCell(6).SetCellValue((sum + Utilities.GetVAT(sum)).ToString("0.00"));
            // sheet1.GetRow(18).GetCell(6).SetCellValue((sum + Utilities.GetVAT(sum)).ToString("0.00"));
            sheet1.GetRow(21).GetCell(6).SetCellValue(price.ToString("0.00"));

            // sheet1.GetRow(49).GetCell(6).SetCellValue((sum + Utilities.GetVAT(sum)).ToString("0.00"));
            // sheet1.GetRow(50).GetCell(6).SetCellValue(Utilities.GetVATRub(sum).ToString("0.00"));

            // sheet1.GetRow(44).GetCell(6).SetCellValue((sum + Utilities.GetVAT(sum)).ToString("0.00"));
            // sheet1.GetRow(45).GetCell(6).SetCellValue((sum + Utilities.GetVAT(sum)).ToString("0.00"));
            //  sheet1.GetRow(46).GetCell(6).SetCellValue((sum + Utilities.GetVAT(sum)).ToString("0.00"));
            //  sheet1.GetRow(47).GetCell(6).SetCellValue((sum + Utilities.GetVAT(sum)).ToString("0.00"));
            //  sheet1.GetRow(48).GetCell(6).SetCellValue((sum + Utilities.GetVAT(sum)).ToString("0.00"));
            sheet1.GetRow(51).GetCell(6).SetCellValue(price.ToString("0.00"));
            /* sheet1.GetRow(2).GetCell(1).SetCellValue(300);
             sheet1.GetRow(3).GetCell(1).SetCellValue(500050);
             sheet1.GetRow(4).GetCell(1).SetCellValue(8000);
             sheet1.GetRow(5).GetCell(1).SetCellValue(110);
             sheet1.GetRow(6).GetCell(1).SetCellValue(100);
             sheet1.GetRow(7).GetCell(1).SetCellValue(200);
             sheet1.GetRow(8).GetCell(1).SetCellValue(210);
             sheet1.GetRow(9).GetCell(1).SetCellValue(2300);
             sheet1.GetRow(10).GetCell(1).SetCellValue(240);
             sheet1.GetRow(11).GetCell(1).SetCellValue(180123);
             sheet1.GetRow(12).GetCell(1).SetCellValue(150);*/

            sheet1.GetRow(23).GetCell(GetLetterNumber("a")).SetCellValue("Сумма прописью: " + RuDateAndMoneyConverter.CurrencyToTxtRub(price, true));
            sheet1.GetRow(53).GetCell(GetLetterNumber("a")).SetCellValue("Сумма прописью: " + RuDateAndMoneyConverter.CurrencyToTxtRub(price, true));

            //Force excel to recalculate all the formula while open
            sheet1.ForceFormulaRecalculation = true;

            WriteToFileUARubles();
        }

        //генерация рублевого счета фактуры
        public static void GenerateBillRubles(int id)
        {
            InitializeWorkbookBillRubles();

            ISheet sheet1 = hssfworkbook.GetSheet("s");


            UniversalEntity ue = new UniversalEntity();
            //UniversalEntity uev = new UniversalEntity();

            UAbonent ua = new UAbonent();
            UAbonentDO uado = new UAbonentDO();
            UOrder uo = new UOrder();
            UOrderDO uodo = new UOrderDO();
            UOrderDetailsDO uodDO = new UOrderDetailsDO();

            ue = uado.RetrieveByOrderID(id);
            string header = string.Empty;
            string actNumber = string.Empty;
            if (ue.Count > 0)
            {
                ua = (UAbonent)ue[0];
                header += ua.Title + " \nюр адрес: " + ua.Address + " \n№ св. пл. НДС: " + ua.VATPay + " \n№ ИНН: " + ua.INN + " \nтел.: " + ua.Phone;
            }
            ue = uodo.RetrieveUOrderById(id);
            if (ue.Count > 0)
            {
                uo = (UOrder)ue[0];
                sheet1.GetRow(4).GetCell(GetLetterNumber("aj")).SetCellValue(uo.ID);
                sheet1.GetRow(9).GetCell(GetLetterNumber("ai")).SetCellValue(RuDateAndMoneyConverter.DateToTextLong(uo.DateIn) + "г.");


            }
            ue = uodDO.RetrieveUOrderDetailsByOrderID(id);
            sheet1.GetRow(16).GetCell(6).SetCellValue(header);



            int count = ue.Count;
            double sum = 0;
            int row = 20;
            if (count <= 5)
            {
                CustomRetrieverDO crDO = new CustomRetrieverDO();
                ue = crDO.RetrieveUActByOrderID5LowRub(id);
                if (ue.Count > 0)
                {
                    for (int i = row; i < ue.Count + row; i++)
                    {
                        ArrayList al = (ArrayList)ue[i - row];

                        sheet1.GetRow(i).GetCell(GetLetterNumber("a")).SetCellValue(string.Format(" *377;08;1 Определения метрологических характеристик водомера  D{0}", al[0].ToString()));
                        sheet1.GetRow(i).GetCell(GetLetterNumber("ad")).SetCellValue("шт.");
                        sheet1.GetRow(i).GetCell(GetLetterNumber("aj")).SetCellValue(al[4].ToString());
                        sheet1.GetRow(i).GetCell(GetLetterNumber("ap")).SetCellValue(Convert.ToDouble(al[3]).ToString("0.00"));
                        sheet1.GetRow(i).GetCell(GetLetterNumber("av")).SetCellValue(Convert.ToDouble(al[3]).ToString("0.00"));
                        sum += Convert.ToDouble(al[3]);
                    }
                }
            }
            else
            {
                CustomRetrieverDO crDO = new CustomRetrieverDO();
                ue = crDO.RetrieveUActByOrderID5HighRub(id);
                if (ue.Count > 0)
                {
                    for (int i = row; i < ue.Count + row; i++)
                    {
                        ArrayList al = (ArrayList)ue[i - row];
                        //FOrderDetailsAct u = (FOrderDetailsAct)fodal[i - row];

                        sheet1.GetRow(i).GetCell(GetLetterNumber("a")).SetCellValue(string.Format(" *377;08;1 Определения метрологических характеристик водомера  D{0}", al[0].ToString()));
                        sheet1.GetRow(i).GetCell(GetLetterNumber("ad")).SetCellValue("шт.");
                        sheet1.GetRow(i).GetCell(GetLetterNumber("aj")).SetCellValue(al[3].ToString());
                        sheet1.GetRow(i).GetCell(GetLetterNumber("ap")).SetCellValue(Convert.ToDouble(al[1]).ToString("0.00"));
                        sheet1.GetRow(i).GetCell(GetLetterNumber("av")).SetCellValue(Convert.ToDouble(al[2]).ToString("0.00"));

                        sum += Convert.ToDouble(al[2]);
                    }
                }
            }
            /*int row = 20;
            double sum = 0;




            
            if (ue.Count > 0)
            {
                for (int i = row; i < ue.Count + row; i++)
                {
                    UOrderDetails u = (UOrderDetails)ue[i - row];

                    sheet1.GetRow(i).GetCell(GetLetterNumber("a")).SetCellValue("Определения метрологических характеристик водомера ");
                    sheet1.GetRow(i).GetCell(GetLetterNumber("ad")).SetCellValue("шт.");
                    sheet1.GetRow(i).GetCell(GetLetterNumber("aj")).SetCellValue("1");
                    sheet1.GetRow(i).GetCell(GetLetterNumber("ap")).SetCellValue(u.Price.ToString("0.00"));
                    sheet1.GetRow(i).GetCell(GetLetterNumber("av")).SetCellValue(u.Price.ToString("0.00"));
                    sum += u.Price;
                }
            }*/
            /*sheet1.GetRow(31).GetCell(GetLetterNumber("av")).SetCellValue(sum.ToString("0.00"));
            sheet1.GetRow(32).GetCell(GetLetterNumber("av")).SetCellValue(Utilities.GetVAT(sum).ToString("0.00"));*/
            sheet1.GetRow(32).GetCell(GetLetterNumber("av")).SetCellValue(sum.ToString("0.00"));
            sheet1.GetRow(35).GetCell(GetLetterNumber("a")).SetCellValue("Общая сумма, подлежащая оплате: " + RuDateAndMoneyConverter.CurrencyToTxtRub(sum, true));
            //Загальна сума, що підлягає оплаті


            //Force excel to recalculate all the formula while open
            sheet1.ForceFormulaRecalculation = true;

            WriteToFileBRubles();
        }

        //акт для установки/снятия 
        public static void GenerateWithdrawalact(int id)
        {
            UniversalEntity ue = new UniversalEntity();
            string header = string.Empty;
            string actNumber = string.Empty;
            string actNumber2 = string.Empty;
            string footer = string.Empty;

            FAbonentDO faDO = new FAbonentDO();

            ue = faDO.RetrieveByOrderID(id);
            if (ue.Count > 0)
            {
                FAbonent fa = (FAbonent)ue[0];
                header += fa.FirstName + " " + fa.Surname + " " + fa.LastName + " \nадрес: " + fa.Address + "\nтел.: " + fa.Phone;
            }

            FOrder fo = new FOrder();
            FOrderDO foDO = new FOrderDO();
            FOrderDetailsDO fodDO = new FOrderDetailsDO();

            ue = foDO.RetrieveFOrderById(id);
            if (ue.Count > 0)
            {
                fo = (FOrder)ue[0];
                actNumber = "  г. Донецк            АКТ № " + fo.Prefix + fo.ID + " за _________________________________г.";
                actNumber2 = "                             по договору (письму) №  " + fo.Prefix + fo.ID + " от " + RuDateAndMoneyConverter.DateToTextLong(fo.DateIn) + " г.";
            }

            ue = faDO.RetrieveByOrderID(id);
            if (ue.Count > 0)
            {
                FAbonent fa = (FAbonent)ue[0];
                footer += " Исполнитель:__________________Ломанов В.И. " + "                                           Заказчик:_______________" + fa.FirstName + " " + fa.Surname + " " + fa.LastName;
            }

            ue = fodDO.RetrieveFOrderDetailsByOrderID(id);

            InitializeWorkbookWithdrawalAct(); //InitializeWorkbook();
            ISheet sheet1 = hssfworkbook.GetSheet("s");
            //create cell on rows, since rows do already exist,it's not necessary to create rows again.
            sheet1.GetRow(2).GetCell(3).SetCellValue(header);
            sheet1.GetRow(32).GetCell(3).SetCellValue(header);
            sheet1.GetRow(9).GetCell(0).SetCellValue(actNumber);
            sheet1.GetRow(39).GetCell(0).SetCellValue(actNumber);
            sheet1.GetRow(11).GetCell(0).SetCellValue(actNumber2);
            sheet1.GetRow(41).GetCell(0).SetCellValue(actNumber2);
            sheet1.GetRow(29).GetCell(0).SetCellValue(footer);
            sheet1.GetRow(59).GetCell(0).SetCellValue(footer);

            int rowC = 44;
            int row = 14;
            double sum = 0;
            double grn = 0;
            double getvatrub = 0;
            double sumrub = 0;
            double getvat = 0;
            double finish = 0;

            int count = ue.Count;

            if (count <= 5)
            {
                CustomRetrieverDO crDO = new CustomRetrieverDO();
                ue = crDO.RetrieveWithdrawalActByOrderID5LowSpecial(id);
                if (ue.Count > 0)
                {
                    for (int i = row; i < ue.Count + row; i++)
                    {
                        ArrayList al = (ArrayList)ue[i - row];
                        //FOrderDetailsAct u = (FOrderDetailsAct)fodal[i - row];
                        /*            sumrub = sum;
            getvat = sumrub*0.2;
            getvat = Math.Round(getvat, 2);
            finish = (sumrub + getvat) + (sumrub + getvat);*/

                        double price = 0;
                        double nds = 0;
                        double givememoney = 0;

                        price += Convert.ToDouble(al[3]);
                        nds = price * 0.2;
                        nds = Math.Round(nds, 2);
                        givememoney = (price + nds) + (price + nds);

                        sheet1.GetRow(i).GetCell(0).SetCellValue(i - row + 1);
                        sheet1.GetRow(i).GetCell(1).SetCellValue(string.Format("Демонтаж, монтаж, определения метрологических \nхарактеристик водомера  D{0}", al[0].ToString()));
                        sheet1.GetRow(i).GetCell(2).SetCellValue("калькуляция");
                        sheet1.GetRow(i).GetCell(3).SetCellValue("шт.");
                        sheet1.GetRow(i).GetCell(4).SetCellValue(al[4].ToString());
                        sheet1.GetRow(i).GetCell(5).SetCellValue(Convert.ToDouble(al[3]).ToString("0.00"));
                        sheet1.GetRow(i).GetCell(6).SetCellValue(Convert.ToDouble(al[3]).ToString("0.00"));
                        //sheet1.GetRow(i).GetCell(5).SetCellValue(givememoney.ToString("0.00"));
                        //sheet1.GetRow(i).GetCell(6).SetCellValue(givememoney.ToString("0.00"));


                        sheet1.GetRow(rowC).GetCell(0).SetCellValue(i - row + 1);
                        sheet1.GetRow(rowC).GetCell(1).SetCellValue(string.Format("Демонтаж, монтаж, определения метрологических \nхарактеристик водомера  D{0}", al[0].ToString()));
                        sheet1.GetRow(rowC).GetCell(2).SetCellValue("калькуляция");
                        sheet1.GetRow(rowC).GetCell(3).SetCellValue("шт.");
                        sheet1.GetRow(rowC).GetCell(4).SetCellValue(al[4].ToString());
                        sheet1.GetRow(rowC).GetCell(5).SetCellValue(Convert.ToDouble(al[3]).ToString("0.00"));
                        sheet1.GetRow(rowC).GetCell(6).SetCellValue(Convert.ToDouble(al[3]).ToString("0.00"));
                        // sheet1.GetRow(rowC).GetCell(5).SetCellValue(givememoney.ToString("0.00"));
                        //sheet1.GetRow(rowC).GetCell(6).SetCellValue(givememoney.ToString("0.00"));


                        sum += Convert.ToDouble(al[3]);
                        rowC++;
                    }
                }
            }
            else
            {
                CustomRetrieverDO crDO = new CustomRetrieverDO();
                ue = crDO.RetrieveWithdrawalActByOrderID5HighSpecial(id);
                if (ue.Count > 0)
                {
                    for (int i = row; i < ue.Count + row; i++)
                    {
                        ArrayList al = (ArrayList)ue[i - row];
                        //FOrderDetailsAct u = (FOrderDetailsAct)fodal[i - row];

                        //рублевый счет/блок для al[1]
                        double price = 0;
                        double nds = 0;
                        double givememoney = 0;

                        price += Convert.ToDouble(al[1]);
                        nds = price * 0.2;
                        nds = Math.Round(nds, 2);
                        givememoney = (price + nds) + (price + nds);


                        //рублевый счет/блок для al[2]
                        double business = 0;
                        double makebusiness = 0;
                        double somemoney = 0;

                        business += Convert.ToDouble(al[2]);
                        makebusiness = business * 0.2;
                        makebusiness = Math.Round(makebusiness, 2);
                        somemoney = (business + makebusiness) + (business + makebusiness);


                        sheet1.GetRow(i).GetCell(0).SetCellValue(i - row + 1);
                        sheet1.GetRow(i).GetCell(1).SetCellValue(string.Format("Демонтаж, монтаж, определения метрологических \nхарактеристик водомера  D{0}", al[0].ToString()));
                        sheet1.GetRow(i).GetCell(2).SetCellValue("калькуляция");
                        sheet1.GetRow(i).GetCell(3).SetCellValue("шт.");
                        sheet1.GetRow(i).GetCell(4).SetCellValue(al[3].ToString());
                        sheet1.GetRow(i).GetCell(5).SetCellValue(Convert.ToDouble(al[1]).ToString("0.00"));
                        sheet1.GetRow(i).GetCell(6).SetCellValue(Convert.ToDouble(al[2]).ToString("0.00"));


                        sheet1.GetRow(rowC).GetCell(0).SetCellValue(i - row + 1);
                        sheet1.GetRow(rowC).GetCell(1).SetCellValue(string.Format("Демонтаж, монтаж, определения метрологических \nхарактеристик водомера  D{0}", al[0].ToString()));
                        sheet1.GetRow(rowC).GetCell(2).SetCellValue("калькуляция");
                        sheet1.GetRow(rowC).GetCell(3).SetCellValue("шт.");
                        sheet1.GetRow(rowC).GetCell(4).SetCellValue(al[3].ToString());
                        sheet1.GetRow(rowC).GetCell(5).SetCellValue(Convert.ToDouble(al[1]).ToString("0.00"));
                        sheet1.GetRow(rowC).GetCell(6).SetCellValue(Convert.ToDouble(al[2]).ToString("0.00"));

                        sum += Convert.ToDouble(al[2]);
                        rowC++;
                    }
                }
            }

            //рублевый счет для вывода итоговый суммы
            sumrub = sum;
            getvat = sumrub * 0.2;
            getvat = Math.Round(getvat, 2);
            finish = (sumrub + getvat) + (sumrub + getvat);
            //для вывода ндс и цены рублевых
            grn = sum * 2;
            getvatrub = grn * 0.2;

            // sheet1.GetRow(19).GetCell(6).SetCellValue(grn.ToString("0.00"));
            //sheet1.GetRow(20).GetCell(6).SetCellValue(getvatrub.ToString("0.00"));
            sheet1.GetRow(21).GetCell(6).SetCellValue(sumrub.ToString("0.00"));

            //sheet1.GetRow(49).GetCell(6).SetCellValue(grn.ToString("0.00"));
            //sheet1.GetRow(50).GetCell(6).SetCellValue(getvatrub.ToString("0.00"));
            sheet1.GetRow(51).GetCell(6).SetCellValue(sumrub.ToString("0.00"));

            //sheet1.GetRow(35).GetCell(GetLetterNumber("a")).SetCellValue("Загальна сума, що підлягає оплаті: " + UADateAndMoneyConverter.CurrencyToTxt((sum + Utilities.GetVAT(sum)), true));
            sheet1.GetRow(24).GetCell(GetLetterNumber("a")).SetCellValue("Сумма прописью: " + RuDateAndMoneyConverter.CurrencyToTxtRub(sumrub, true));
            sheet1.GetRow(54).GetCell(GetLetterNumber("a")).SetCellValue("Сумма прописью: " + RuDateAndMoneyConverter.CurrencyToTxtRub(sumrub, true));


            //Force excel to recalculate all the formula while open
            sheet1.ForceFormulaRecalculation = true;

            WriteToFileWithdrawal();
        }


        public static void GenerateWithdrawalActSpecial(int id)
        {
            UniversalEntity ue = new UniversalEntity();
            string header = string.Empty;
            string actNumber = string.Empty;
            string actNumber2 = string.Empty;
            string footer = string.Empty;

            AlternativeAbonentDO faDO = new AlternativeAbonentDO();

            ue = faDO.RetrieveBySOrderID(id);
            if (ue.Count > 0)
            {
                AlternativeAbonent fa = (AlternativeAbonent)ue[0];
                header += fa.FirstName + " " + fa.Surname + " " + fa.LastName + " \nадрес: " + fa.Address + "\nтел.: " + fa.Phone;
            }

            AlternativeOrder fo = new AlternativeOrder();
            AlternativeOrderDO foDO = new AlternativeOrderDO();
            AlternativeOrderDetailsDO fodDO = new AlternativeOrderDetailsDO();

            ue = foDO.RetrieveSOrderById(id);
            if (ue.Count > 0)
            {
                fo = (AlternativeOrder)ue[0];
                actNumber = "  г. Донецк            АКТ № " + fo.Prefix + fo.ID + " за _________________________________г.";
                actNumber2 = "                             по договору (письму) №  " + fo.Prefix + fo.ID + " от " + RuDateAndMoneyConverter.DateToTextLong(fo.DateIn) + " г.";
            }

            ue = faDO.RetrieveBySOrderID(id);
            if (ue.Count > 0)
            {
                AlternativeAbonent fa = (AlternativeAbonent)ue[0];
                footer += " Исполнитель:__________________Ломанов В.И. " + "                                           Заказчик:_______________" + fa.FirstName + " " + fa.Surname + " " + fa.LastName;
            }

            ue = fodDO.RetrieveSOrderDetailsBySorderID(id);

            InitializeWorkbookWithdrawalActSpecial(); //InitializeWorkbook();
            ISheet sheet1 = hssfworkbook.GetSheet("s");
            //create cell on rows, since rows do already exist,it's not necessary to create rows again.
            sheet1.GetRow(2).GetCell(3).SetCellValue(header);
            sheet1.GetRow(32).GetCell(3).SetCellValue(header);
            sheet1.GetRow(9).GetCell(0).SetCellValue(actNumber);
            sheet1.GetRow(39).GetCell(0).SetCellValue(actNumber);
            sheet1.GetRow(11).GetCell(0).SetCellValue(actNumber2);
            sheet1.GetRow(41).GetCell(0).SetCellValue(actNumber2);
            sheet1.GetRow(29).GetCell(0).SetCellValue(footer);
            sheet1.GetRow(59).GetCell(0).SetCellValue(footer);

            int rowC = 44;
            int row = 14;
            double sum = 0;
            double grn = 0;
            double getvatrub = 0;
            double sumrub = 0;
            double getvat = 0;
            double finish = 0;

            int count = ue.Count;

            if (count <= 5)
            {
                CustomRetrieverDO crDO = new CustomRetrieverDO();
                ue = crDO.RetrieveWithdrawalActBySOrderID5LowWithdrawalSpecial(id);
                if (ue.Count > 0)
                {
                    for (int i = row; i < ue.Count + row; i++)
                    {
                        ArrayList al = (ArrayList)ue[i - row];
                        //FOrderDetailsAct u = (FOrderDetailsAct)fodal[i - row];
                        /*            sumrub = sum;
            getvat = sumrub*0.2;
            getvat = Math.Round(getvat, 2);
            finish = (sumrub + getvat) + (sumrub + getvat);*/

                        double price = 0;
                        double nds = 0;
                        double givememoney = 0;

                        price += Convert.ToDouble(al[3]);
                        nds = price * 0.2;
                        nds = Math.Round(nds, 2);
                        givememoney = (price + nds) + (price + nds);

                        sheet1.GetRow(i).GetCell(0).SetCellValue(i - row + 1);
                        sheet1.GetRow(i).GetCell(1).SetCellValue(string.Format("Демонтаж, монтаж, определения метрологических \nхарактеристик водомера  D{0}", al[0].ToString()));
                        sheet1.GetRow(i).GetCell(2).SetCellValue("калькуляция");
                        sheet1.GetRow(i).GetCell(3).SetCellValue("шт.");
                        sheet1.GetRow(i).GetCell(4).SetCellValue(al[4].ToString());
                        sheet1.GetRow(i).GetCell(5).SetCellValue(Convert.ToDouble(al[3]).ToString("0.00"));
                        sheet1.GetRow(i).GetCell(6).SetCellValue(Convert.ToDouble(al[3]).ToString("0.00"));
                        //sheet1.GetRow(i).GetCell(5).SetCellValue(givememoney.ToString("0.00"));
                        //sheet1.GetRow(i).GetCell(6).SetCellValue(givememoney.ToString("0.00"));


                        sheet1.GetRow(rowC).GetCell(0).SetCellValue(i - row + 1);
                        sheet1.GetRow(rowC).GetCell(1).SetCellValue(string.Format("Демонтаж, монтаж, определения метрологических \nхарактеристик водомера  D{0}", al[0].ToString()));
                        sheet1.GetRow(rowC).GetCell(2).SetCellValue("калькуляция");
                        sheet1.GetRow(rowC).GetCell(3).SetCellValue("шт.");
                        sheet1.GetRow(rowC).GetCell(4).SetCellValue(al[4].ToString());
                        sheet1.GetRow(rowC).GetCell(5).SetCellValue(Convert.ToDouble(al[3]).ToString("0.00"));
                        sheet1.GetRow(rowC).GetCell(6).SetCellValue(Convert.ToDouble(al[3]).ToString("0.00"));
                        // sheet1.GetRow(rowC).GetCell(5).SetCellValue(givememoney.ToString("0.00"));
                        //sheet1.GetRow(rowC).GetCell(6).SetCellValue(givememoney.ToString("0.00"));


                        sum += Convert.ToDouble(al[3]);
                        rowC++;
                    }
                }
            }
            else
            {
                CustomRetrieverDO crDO = new CustomRetrieverDO();
                ue = crDO.RetrieveWithdrawalActBySOrderID5HighWithdrawalSpecial(id);
                if (ue.Count > 0)
                {
                    for (int i = row; i < ue.Count + row; i++)
                    {
                        ArrayList al = (ArrayList)ue[i - row];
                        //FOrderDetailsAct u = (FOrderDetailsAct)fodal[i - row];

                        //рублевый счет/блок для al[1]
                        double price = 0;
                        double nds = 0;
                        double givememoney = 0;

                        price += Convert.ToDouble(al[1]);
                        nds = price * 0.2;
                        nds = Math.Round(nds, 2);
                        givememoney = (price + nds) + (price + nds);


                        //рублевый счет/блок для al[2]
                        double business = 0;
                        double makebusiness = 0;
                        double somemoney = 0;

                        business += Convert.ToDouble(al[2]);
                        makebusiness = business * 0.2;
                        makebusiness = Math.Round(makebusiness, 2);
                        somemoney = (business + makebusiness) + (business + makebusiness);


                        sheet1.GetRow(i).GetCell(0).SetCellValue(i - row + 1);
                        sheet1.GetRow(i).GetCell(1).SetCellValue(string.Format("Демонтаж, монтаж, определения метрологических \nхарактеристик водомера  D{0}", al[0].ToString()));
                        sheet1.GetRow(i).GetCell(2).SetCellValue("калькуляция");
                        sheet1.GetRow(i).GetCell(3).SetCellValue("шт.");
                        sheet1.GetRow(i).GetCell(4).SetCellValue(al[3].ToString());
                        sheet1.GetRow(i).GetCell(5).SetCellValue(Convert.ToDouble(al[1]).ToString("0.00"));
                        sheet1.GetRow(i).GetCell(6).SetCellValue(Convert.ToDouble(al[2]).ToString("0.00"));


                        sheet1.GetRow(rowC).GetCell(0).SetCellValue(i - row + 1);
                        sheet1.GetRow(rowC).GetCell(1).SetCellValue(string.Format("Демонтаж, монтаж, определения метрологических \nхарактеристик водомера  D{0}", al[0].ToString()));
                        sheet1.GetRow(rowC).GetCell(2).SetCellValue("калькуляция");
                        sheet1.GetRow(rowC).GetCell(3).SetCellValue("шт.");
                        sheet1.GetRow(rowC).GetCell(4).SetCellValue(al[3].ToString());
                        sheet1.GetRow(rowC).GetCell(5).SetCellValue(Convert.ToDouble(al[1]).ToString("0.00"));
                        sheet1.GetRow(rowC).GetCell(6).SetCellValue(Convert.ToDouble(al[2]).ToString("0.00"));

                        sum += Convert.ToDouble(al[2]);
                        rowC++;
                    }
                }
            }

            //рублевый счет для вывода итоговый суммы
            sumrub = sum;
            getvat = sumrub * 0.2;
            getvat = Math.Round(getvat, 2);
            finish = (sumrub + getvat) + (sumrub + getvat);
            //для вывода ндс и цены рублевых
            grn = sum * 2;
            getvatrub = grn * 0.2;

            // sheet1.GetRow(19).GetCell(6).SetCellValue(grn.ToString("0.00"));
            //sheet1.GetRow(20).GetCell(6).SetCellValue(getvatrub.ToString("0.00"));
            sheet1.GetRow(21).GetCell(6).SetCellValue(sumrub.ToString("0.00"));

            //sheet1.GetRow(49).GetCell(6).SetCellValue(grn.ToString("0.00"));
            //sheet1.GetRow(50).GetCell(6).SetCellValue(getvatrub.ToString("0.00"));
            sheet1.GetRow(51).GetCell(6).SetCellValue(sumrub.ToString("0.00"));

            //sheet1.GetRow(35).GetCell(GetLetterNumber("a")).SetCellValue("Загальна сума, що підлягає оплаті: " + UADateAndMoneyConverter.CurrencyToTxt((sum + Utilities.GetVAT(sum)), true));
            sheet1.GetRow(24).GetCell(GetLetterNumber("a")).SetCellValue("Сумма прописью: " + RuDateAndMoneyConverter.CurrencyToTxtRub(sumrub, true));
            sheet1.GetRow(54).GetCell(GetLetterNumber("a")).SetCellValue("Сумма прописью: " + RuDateAndMoneyConverter.CurrencyToTxtRub(sumrub, true));


            //Force excel to recalculate all the formula while open
            sheet1.ForceFormulaRecalculation = true;

            WriteToFileWithdrawalSpecial();
        }

        public static void GenerateReplacementAct(int id)
        {
            UniversalEntity ue = new UniversalEntity();
            string header = string.Empty;
            string actNumber = string.Empty;
            string actNumber2 = string.Empty;
            string footer = string.Empty;

            AlternativeAbonentDO faDO = new AlternativeAbonentDO();

            ue = faDO.RetrieveBySOrderID(id);
            if (ue.Count > 0)
            {
                AlternativeAbonent fa = (AlternativeAbonent)ue[0];
                header += fa.FirstName + " " + fa.Surname + " " + fa.LastName + " \nадрес: " + fa.Address + "\nтел.: " + fa.Phone;
            }

            AlternativeOrder fo = new AlternativeOrder();
            AlternativeOrderDO foDO = new AlternativeOrderDO();
            AlternativeOrderDetailsDO fodDO = new AlternativeOrderDetailsDO();

            ue = foDO.RetrieveSOrderById(id);
            if (ue.Count > 0)
            {
                fo = (AlternativeOrder)ue[0];
                actNumber = "  г. Донецк            АКТ № " + fo.Prefix + fo.ID + " за _________________________________г.";
                actNumber2 = "                             по договору (письму) №  " + fo.Prefix + fo.ID + " от " + RuDateAndMoneyConverter.DateToTextLong(fo.DateIn) + " г.";
            }

            ue = faDO.RetrieveBySOrderID(id);
            if (ue.Count > 0)
            {
                AlternativeAbonent fa = (AlternativeAbonent)ue[0];
                footer += " Исполнитель:__________________Ломанов В.И. " + "                                           Заказчик:_______________" + fa.FirstName + " " + fa.Surname + " " + fa.LastName;
            }

            ue = fodDO.RetrieveSOrderDetailsBySorderID(id);

            InitializeWorkbookReplacementAct(); //InitializeWorkbook();
            ISheet sheet1 = hssfworkbook.GetSheet("s");
            //create cell on rows, since rows do already exist,it's not necessary to create rows again.
            sheet1.GetRow(2).GetCell(3).SetCellValue(header);
            sheet1.GetRow(32).GetCell(3).SetCellValue(header);
            sheet1.GetRow(9).GetCell(0).SetCellValue(actNumber);
            sheet1.GetRow(39).GetCell(0).SetCellValue(actNumber);
            sheet1.GetRow(11).GetCell(0).SetCellValue(actNumber2);
            sheet1.GetRow(41).GetCell(0).SetCellValue(actNumber2);
            sheet1.GetRow(29).GetCell(0).SetCellValue(footer);
            sheet1.GetRow(59).GetCell(0).SetCellValue(footer);

            int rowC = 44;
            int row = 14;
            double sum = 0;
            double grn = 0;
            double getvatrub = 0;
            double sumrub = 0;
            double getvat = 0;
            double finish = 0;

            int count = ue.Count;

            if (count <= 5)
            {
                CustomRetrieverDO crDO = new CustomRetrieverDO();
                ue = crDO.RetrieveReplacementActBySOrderID5Low(id);
                if (ue.Count > 0)
                {
                    for (int i = row; i < ue.Count + row; i++)
                    {
                        ArrayList al = (ArrayList)ue[i - row];
                        //FOrderDetailsAct u = (FOrderDetailsAct)fodal[i - row];
                        /*            sumrub = sum;
            getvat = sumrub*0.2;
            getvat = Math.Round(getvat, 2);
            finish = (sumrub + getvat) + (sumrub + getvat);*/

                        double price = 0;
                        double nds = 0;
                        double givememoney = 0;

                        price += Convert.ToDouble(al[3]);
                        nds = price * 0.2;
                        nds = Math.Round(nds, 2);
                        givememoney = (price + nds) + (price + nds);

                        sheet1.GetRow(i).GetCell(0).SetCellValue(i - row + 1);
                        sheet1.GetRow(i).GetCell(1).SetCellValue(string.Format("Демонтаж, монтаж водомера  D{0}", al[0].ToString()));
                        sheet1.GetRow(i).GetCell(2).SetCellValue("калькуляция");
                        sheet1.GetRow(i).GetCell(3).SetCellValue("шт.");
                        sheet1.GetRow(i).GetCell(4).SetCellValue(al[4].ToString());
                        sheet1.GetRow(i).GetCell(5).SetCellValue(Convert.ToDouble(al[3]).ToString("0.00"));
                        sheet1.GetRow(i).GetCell(6).SetCellValue(Convert.ToDouble(al[3]).ToString("0.00"));
                        //sheet1.GetRow(i).GetCell(5).SetCellValue(givememoney.ToString("0.00"));
                        //sheet1.GetRow(i).GetCell(6).SetCellValue(givememoney.ToString("0.00"));


                        sheet1.GetRow(rowC).GetCell(0).SetCellValue(i - row + 1);
                        sheet1.GetRow(rowC).GetCell(1).SetCellValue(string.Format("Демонтаж, монтаж водомера  D{0}", al[0].ToString()));
                        sheet1.GetRow(rowC).GetCell(2).SetCellValue("калькуляция");
                        sheet1.GetRow(rowC).GetCell(3).SetCellValue("шт.");
                        sheet1.GetRow(rowC).GetCell(4).SetCellValue(al[4].ToString());
                        sheet1.GetRow(rowC).GetCell(5).SetCellValue(Convert.ToDouble(al[3]).ToString("0.00"));
                        sheet1.GetRow(rowC).GetCell(6).SetCellValue(Convert.ToDouble(al[3]).ToString("0.00"));
                        // sheet1.GetRow(rowC).GetCell(5).SetCellValue(givememoney.ToString("0.00"));
                        //sheet1.GetRow(rowC).GetCell(6).SetCellValue(givememoney.ToString("0.00"));


                        sum += Convert.ToDouble(al[3]);
                        rowC++;
                    }
                }
            }
            else
            {
                CustomRetrieverDO crDO = new CustomRetrieverDO();
                ue = crDO.RetrieveReplacementActBySOrderID5High(id);
                if (ue.Count > 0)
                {
                    for (int i = row; i < ue.Count + row; i++)
                    {
                        ArrayList al = (ArrayList)ue[i - row];
                        //FOrderDetailsAct u = (FOrderDetailsAct)fodal[i - row];

                        //рублевый счет/блок для al[1]
                        double price = 0;
                        double nds = 0;
                        double givememoney = 0;

                        price += Convert.ToDouble(al[1]);
                        nds = price * 0.2;
                        nds = Math.Round(nds, 2);
                        givememoney = (price + nds) + (price + nds);


                        //рублевый счет/блок для al[2]
                        double business = 0;
                        double makebusiness = 0;
                        double somemoney = 0;

                        business += Convert.ToDouble(al[2]);
                        makebusiness = business * 0.2;
                        makebusiness = Math.Round(makebusiness, 2);
                        somemoney = (business + makebusiness) + (business + makebusiness);


                        sheet1.GetRow(i).GetCell(0).SetCellValue(i - row + 1);
                        sheet1.GetRow(i).GetCell(1).SetCellValue(string.Format("Демонтаж, монтаж водомера  D{0}", al[0].ToString()));
                        sheet1.GetRow(i).GetCell(2).SetCellValue("калькуляция");
                        sheet1.GetRow(i).GetCell(3).SetCellValue("шт.");
                        sheet1.GetRow(i).GetCell(4).SetCellValue(al[3].ToString());
                        sheet1.GetRow(i).GetCell(5).SetCellValue(Convert.ToDouble(al[1]).ToString("0.00"));
                        sheet1.GetRow(i).GetCell(6).SetCellValue(Convert.ToDouble(al[2]).ToString("0.00"));


                        sheet1.GetRow(rowC).GetCell(0).SetCellValue(i - row + 1);
                        sheet1.GetRow(rowC).GetCell(1).SetCellValue(string.Format("Демонтаж, монтаж водомера  D{0}", al[0].ToString()));
                        sheet1.GetRow(rowC).GetCell(2).SetCellValue("калькуляция");
                        sheet1.GetRow(rowC).GetCell(3).SetCellValue("шт.");
                        sheet1.GetRow(rowC).GetCell(4).SetCellValue(al[3].ToString());
                        sheet1.GetRow(rowC).GetCell(5).SetCellValue(Convert.ToDouble(al[1]).ToString("0.00"));
                        sheet1.GetRow(rowC).GetCell(6).SetCellValue(Convert.ToDouble(al[2]).ToString("0.00"));

                        sum += Convert.ToDouble(al[2]);
                        rowC++;
                    }
                }
            }

            //рублевый счет для вывода итоговый суммы
            sumrub = sum;
            getvat = sumrub * 0.2;
            getvat = Math.Round(getvat, 2);
            finish = (sumrub + getvat) + (sumrub + getvat);
            //для вывода ндс и цены рублевых
            grn = sum * 2;
            getvatrub = grn * 0.2;

            // sheet1.GetRow(19).GetCell(6).SetCellValue(grn.ToString("0.00"));
            //sheet1.GetRow(20).GetCell(6).SetCellValue(getvatrub.ToString("0.00"));
            sheet1.GetRow(21).GetCell(6).SetCellValue(sumrub.ToString("0.00"));

            //sheet1.GetRow(49).GetCell(6).SetCellValue(grn.ToString("0.00"));
            //sheet1.GetRow(50).GetCell(6).SetCellValue(getvatrub.ToString("0.00"));
            sheet1.GetRow(51).GetCell(6).SetCellValue(sumrub.ToString("0.00"));

            //sheet1.GetRow(35).GetCell(GetLetterNumber("a")).SetCellValue("Загальна сума, що підлягає оплаті: " + UADateAndMoneyConverter.CurrencyToTxt((sum + Utilities.GetVAT(sum)), true));
            sheet1.GetRow(24).GetCell(GetLetterNumber("a")).SetCellValue("Сумма прпоисью: " + RuDateAndMoneyConverter.CurrencyToTxtRub(sumrub, true));
            sheet1.GetRow(54).GetCell(GetLetterNumber("a")).SetCellValue("Сумма прописью: " + RuDateAndMoneyConverter.CurrencyToTxtRub(sumrub, true));


            //Force excel to recalculate all the formula while open
            sheet1.ForceFormulaRecalculation = true;

            WriteToFileReplacementAct();
        }

        public static void GenerateBillForLittleWDK(int id)
        {
            InitializeWorkbookBillForLittleWDK();

            ISheet sheet1 = hssfworkbook.GetSheet("s");


            UniversalEntity ue = new UniversalEntity();
            //UniversalEntity uev = new UniversalEntity();

            UAbonent ua = new UAbonent();
            UAbonentDO uado = new UAbonentDO();
            UOrder uo = new UOrder();
            UOrderDO uodo = new UOrderDO();
            UOrderDetailsDO uodDO = new UOrderDetailsDO();

            ue = uado.RetrieveByOrderID(id);
            string header = string.Empty;
            string actNumber = string.Empty;
            if (ue.Count > 0)
            {
                ua = (UAbonent)ue[0];
                header += ua.Title + " \nюр адрес: " + ua.Address + " \n№ св. пл. НДС: " + ua.VATPay + " \n№ ИНН: " + ua.INN + " \nтел.: " + ua.Phone;
            }
            ue = uodo.RetrieveUOrderById(id);
            if (ue.Count > 0)
            {
                uo = (UOrder)ue[0];
                sheet1.GetRow(4).GetCell(GetLetterNumber("aj")).SetCellValue(uo.ID);
                sheet1.GetRow(9).GetCell(GetLetterNumber("ai")).SetCellValue(RuDateAndMoneyConverter.DateToTextLong(uo.DateIn) + "г.");


            }
            ue = uodDO.RetrieveUOrderDetailsByOrderID(id);
            sheet1.GetRow(16).GetCell(6).SetCellValue(header);



            int count = ue.Count;
            double sum = 0;
            int row = 20;
            if (count <= 5)
            {
                CustomRetrieverDO crDO = new CustomRetrieverDO();
                ue = crDO.RetrieveUActByOrderID5LowRub(id);
                if (ue.Count > 0)
                {
                    for (int i = row; i < ue.Count + row; i++)
                    {
                        ArrayList al = (ArrayList)ue[i - row];

                        sheet1.GetRow(i).GetCell(GetLetterNumber("a")).SetCellValue(string.Format(" *377;08;1 Определения метрологических характеристик водомера  D{0}", al[0].ToString()));
                        sheet1.GetRow(i).GetCell(GetLetterNumber("ad")).SetCellValue("шт.");
                        sheet1.GetRow(i).GetCell(GetLetterNumber("aj")).SetCellValue(al[4].ToString());
                        sheet1.GetRow(i).GetCell(GetLetterNumber("ap")).SetCellValue(Convert.ToDouble(al[3]).ToString("0.00"));
                        sheet1.GetRow(i).GetCell(GetLetterNumber("av")).SetCellValue(Convert.ToDouble(al[3]).ToString("0.00"));
                        sum += Convert.ToDouble(al[3]);
                    }
                }
            }
            else
            {
                CustomRetrieverDO crDO = new CustomRetrieverDO();
                ue = crDO.RetrieveUActByOrderID5HighRub(id);
                if (ue.Count > 0)
                {
                    for (int i = row; i < ue.Count + row; i++)
                    {
                        ArrayList al = (ArrayList)ue[i - row];
                        //FOrderDetailsAct u = (FOrderDetailsAct)fodal[i - row];

                        sheet1.GetRow(i).GetCell(GetLetterNumber("a")).SetCellValue(string.Format(" *377;08;1 Определения метрологических характеристик водомера  D{0}", al[0].ToString()));
                        sheet1.GetRow(i).GetCell(GetLetterNumber("ad")).SetCellValue("шт.");
                        sheet1.GetRow(i).GetCell(GetLetterNumber("aj")).SetCellValue(al[3].ToString());
                        sheet1.GetRow(i).GetCell(GetLetterNumber("ap")).SetCellValue(Convert.ToDouble(al[1]).ToString("0.00"));
                        sheet1.GetRow(i).GetCell(GetLetterNumber("av")).SetCellValue(Convert.ToDouble(al[2]).ToString("0.00"));

                        sum += Convert.ToDouble(al[2]);
                    }
                }
            }
            /*int row = 20;
            double sum = 0;




            
            if (ue.Count > 0)
            {
                for (int i = row; i < ue.Count + row; i++)
                {
                    UOrderDetails u = (UOrderDetails)ue[i - row];

                    sheet1.GetRow(i).GetCell(GetLetterNumber("a")).SetCellValue("Определения метрологических характеристик водомера ");
                    sheet1.GetRow(i).GetCell(GetLetterNumber("ad")).SetCellValue("шт.");
                    sheet1.GetRow(i).GetCell(GetLetterNumber("aj")).SetCellValue("1");
                    sheet1.GetRow(i).GetCell(GetLetterNumber("ap")).SetCellValue(u.Price.ToString("0.00"));
                    sheet1.GetRow(i).GetCell(GetLetterNumber("av")).SetCellValue(u.Price.ToString("0.00"));
                    sum += u.Price;
                }
            }*/
            /*sheet1.GetRow(31).GetCell(GetLetterNumber("av")).SetCellValue(sum.ToString("0.00"));
            sheet1.GetRow(32).GetCell(GetLetterNumber("av")).SetCellValue(Utilities.GetVAT(sum).ToString("0.00"));*/
            sheet1.GetRow(32).GetCell(GetLetterNumber("av")).SetCellValue(sum.ToString("0.00"));
            sheet1.GetRow(35).GetCell(GetLetterNumber("a")).SetCellValue("Общая сумма, подлежащая оплате: " + RuDateAndMoneyConverter.CurrencyToTxtRub(sum, true));
            //Загальна сума, що підлягає оплаті


            //Force excel to recalculate all the formula while open
            sheet1.ForceFormulaRecalculation = true;

            WriteToFileBillForLittleWDK();
        }

        public static void GenerateActRublesLittleWDK(int id)
        {
            UniversalEntity ue = new UniversalEntity();
            UAbonent ua = new UAbonent();
            UAbonentDO uado = new UAbonentDO();
            UOrder uo = new UOrder();
            UOrderDO uodo = new UOrderDO();
            UOrderDetailsDO uodDO = new UOrderDetailsDO();

            ue = uado.RetrieveByOrderID(id);
            string header = string.Empty;
            string actNumber = string.Empty;
            if (ue.Count > 0)
            {
                ua = (UAbonent)ue[0];
                header += ua.Title + " \nюр адрес: " + ua.Address + " \n№ ИНН: " + ua.INN + " \nтел.: " + ua.Phone;
            }
            ue = uodo.RetrieveUOrderById(id);
            if (ue.Count > 0)
            {
                uo = (UOrder)ue[0];
                if (uo.DateOut.HasValue)
                {
                    actNumber = "  г. Донецк            АКТ № " + uo.ID + " за _________________________________г.";
                }
                else
                {
                    actNumber = "  г. Донецк            АКТ № " + uo.ID + " за _________________________________ г.";
                }
            }

            ue = uodDO.RetrieveUOrderDetailsByOrderID(id);

            InitializeWorkBookUActRublesForLittleWDK();

            ISheet sheet1 = hssfworkbook.GetSheet("s");

            sheet1.GetRow(11).GetCell(0).SetCellValue("                             по договору (письму) № " + uo.ID + " от " + RuDateAndMoneyConverter.DateToTextLong(uo.DateIn) + " г.");

            sheet1.GetRow(41).GetCell(0).SetCellValue("                             по договору (письму) № " + uo.ID + " от " + RuDateAndMoneyConverter.DateToTextLong(uo.DateIn) + " г.");

            //create cell on rows, since rows do already exist,it's not necessary to create rows again.
            sheet1.GetRow(2).GetCell(3).SetCellValue(header);
            sheet1.GetRow(32).GetCell(3).SetCellValue(header);
            sheet1.GetRow(9).GetCell(0).SetCellValue(actNumber);
            sheet1.GetRow(39).GetCell(0).SetCellValue(actNumber);

            int rowC = 44;
            int row = 14;
            double sum = 0;
            //double vat = 0;
            //double result = 0;
            int count = ue.Count;

            if (count <= 5)
            {
                CustomRetrieverDO crDO = new CustomRetrieverDO();
                ue = crDO.RetrieveUActByOrderID5LowRub(id);
                if (ue.Count > 0)
                {
                    //uo = (UOrder)ue[0];
                    //ue = uodDO.RetrieveUOrderDetailsByOrderID(id);
                    /* foreach (UOrderDetails uod in ue)
                     {
                         sum += uod.Price;
                     }*/
                    for (int i = row; i < ue.Count + row; i++)
                    {
                        ArrayList al = (ArrayList)ue[i - row];

                        //FOrderDetailsAct u = (FOrderDetailsAct)fodal[i - row];

                        double sumone = 0;
                        double getnds = 0;
                        double getfin = 0;

                        sumone += Convert.ToDouble(al[3]);
                        getnds = sumone * 0.2;
                        getnds = Math.Round(getnds, 2);
                        getfin = (getnds + sumone) + (getnds + sumone);

                        sheet1.GetRow(i).GetCell(0).SetCellValue(i - row + 1);
                        sheet1.GetRow(i).GetCell(1).SetCellValue(string.Format("Определения метрологических характеристик водомера D{0}", al[0].ToString()));
                        sheet1.GetRow(i).GetCell(2).SetCellValue("калькуляция");
                        sheet1.GetRow(i).GetCell(3).SetCellValue("шт.");
                        sheet1.GetRow(i).GetCell(4).SetCellValue(al[4].ToString());
                        sheet1.GetRow(i).GetCell(5).SetCellValue(Convert.ToDouble(al[3]).ToString("0.00"));
                        sheet1.GetRow(i).GetCell(6).SetCellValue(Convert.ToDouble(al[3]).ToString("0.00"));
                        sheet1.GetRow(i).GetCell(7).SetCellValue(getfin.ToString("0.00"));
                        sheet1.GetRow(i).GetCell(8).SetCellValue(getfin.ToString("0.00"));

                        sheet1.GetRow(rowC).GetCell(0).SetCellValue(i - row + 1);
                        sheet1.GetRow(rowC).GetCell(1).SetCellValue(string.Format("Определения метрологических характеристик водомера  D{0}", al[0].ToString()));
                        sheet1.GetRow(rowC).GetCell(2).SetCellValue("калькуляция");
                        sheet1.GetRow(rowC).GetCell(3).SetCellValue("шт.");
                        sheet1.GetRow(rowC).GetCell(4).SetCellValue(al[4].ToString());
                        sheet1.GetRow(rowC).GetCell(5).SetCellValue(Convert.ToDouble(al[3]).ToString("0.00"));
                        sheet1.GetRow(rowC).GetCell(6).SetCellValue(Convert.ToDouble(al[3]).ToString("0.00"));
                        sheet1.GetRow(rowC).GetCell(7).SetCellValue(getfin.ToString("0.00"));
                        sheet1.GetRow(rowC).GetCell(8).SetCellValue(getfin.ToString("0.00"));

                        sum += Convert.ToDouble(al[3]);

                        rowC++;
                    }
                }
            }
            else
            {
                CustomRetrieverDO crDO = new CustomRetrieverDO();
                ue = crDO.RetrieveUActByOrderID5HighRub(id);
                if (ue.Count > 0)
                {
                    for (int i = row; i < ue.Count + row; i++)
                    {
                        ArrayList al = (ArrayList)ue[i - row];
                        //FOrderDetailsAct u = (FOrderDetailsAct)fodal[i - row];


                        sheet1.GetRow(i).GetCell(0).SetCellValue(i - row + 1);
                        sheet1.GetRow(i).GetCell(1).SetCellValue(string.Format("Определения метрологических характеристик водомера  D{0}", al[0].ToString()));
                        sheet1.GetRow(i).GetCell(2).SetCellValue("калькуляция");
                        sheet1.GetRow(i).GetCell(3).SetCellValue("шт.");
                        sheet1.GetRow(i).GetCell(4).SetCellValue(al[3].ToString());
                        sheet1.GetRow(i).GetCell(5).SetCellValue(Convert.ToDouble(al[1]).ToString("0.00"));
                        sheet1.GetRow(i).GetCell(6).SetCellValue(Convert.ToDouble(al[2]).ToString("0.00"));


                        sheet1.GetRow(rowC).GetCell(0).SetCellValue(i - row + 1);
                        sheet1.GetRow(rowC).GetCell(1).SetCellValue(string.Format("Определения метрологических характеристик водомера  D{0}", al[0].ToString()));
                        sheet1.GetRow(rowC).GetCell(2).SetCellValue("калькуляция");
                        sheet1.GetRow(rowC).GetCell(3).SetCellValue("шт.");
                        sheet1.GetRow(rowC).GetCell(4).SetCellValue(al[3].ToString());
                        sheet1.GetRow(rowC).GetCell(5).SetCellValue(Convert.ToDouble(al[1]).ToString("0.00"));
                        sheet1.GetRow(rowC).GetCell(6).SetCellValue(Convert.ToDouble(al[2]).ToString("0.00"));

                        sum += Convert.ToDouble(al[2]);

                        rowC++;
                    }
                }
            }






            /*int rowC = 44;
            int row = 14;
            double sum = 0;
            if (ue.Count > 0)
            {
                for (int i = row; i < ue.Count + row; i++)
                {
                    UOrderDetails u = (UOrderDetails)ue[i - row];

                    sheet1.GetRow(i).GetCell(0).SetCellValue(i - row+1);
                    sheet1.GetRow(i).GetCell(1).SetCellValue(string.Format("Определения метрологических характеристик водомера  D{0}, {1}, {2}", al[0].ToString(), al[1].ToString(), al[2].ToString()));
                    sheet1.GetRow(i).GetCell(2).SetCellValue("калькуляция");
                    sheet1.GetRow(i).GetCell(3).SetCellValue("шт.");
                    sheet1.GetRow(i).GetCell(4).SetCellValue("1");
                    sheet1.GetRow(i).GetCell(5).SetCellValue(u.Price.ToString("0.00"));
                    sheet1.GetRow(i).GetCell(6).SetCellValue(u.Price.ToString("0.00"));

                    sheet1.GetRow(rowC).GetCell(0).SetCellValue(i - row + 1);
                    sheet1.GetRow(rowC).GetCell(1).SetCellValue(string.Format("Определения метрологических характеристик водомера  D{0}, {1}, {2}", al[0].ToString(), al[1].ToString(), al[2].ToString()));
                    sheet1.GetRow(rowC).GetCell(2).SetCellValue("калькуляция");
                    sheet1.GetRow(rowC).GetCell(3).SetCellValue("шт.");
                    sheet1.GetRow(rowC).GetCell(4).SetCellValue("1");
                    sheet1.GetRow(rowC).GetCell(5).SetCellValue(u.Price.ToString("0.00"));
                    sheet1.GetRow(rowC).GetCell(6).SetCellValue(u.Price.ToString("0.00"));

                    sum += u.Price;
                    rowC++;
                }
            }*/
            double price = 0;
            double nds = 0;
            double finalsum = 0;

            price = sum;
            nds = price * 0.2;
            nds = Math.Round(nds, 2);
            finalsum = (nds + price) + (price + nds);


            // sheet1.GetRow(19).GetCell(6).SetCellValue((sum + Utilities.GetVAT(sum)).ToString("0.00"));
            // sheet1.GetRow(20).GetCell(6).SetCellValue(Utilities.GetVATRub(sum).ToString("0.00"));

            // sheet1.GetRow(14).GetCell(6).SetCellValue((sum + Utilities.GetVAT(sum)).ToString("0.00"));
            //  sheet1.GetRow(15).GetCell(6).SetCellValue((sum + Utilities.GetVAT(sum)).ToString("0.00"));
            //  sheet1.GetRow(16).GetCell(6).SetCellValue((sum + Utilities.GetVAT(sum)).ToString("0.00"));
            //  sheet1.GetRow(17).GetCell(6).SetCellValue((sum + Utilities.GetVAT(sum)).ToString("0.00"));
            // sheet1.GetRow(18).GetCell(6).SetCellValue((sum + Utilities.GetVAT(sum)).ToString("0.00"));
            sheet1.GetRow(21).GetCell(6).SetCellValue(price.ToString("0.00"));

            // sheet1.GetRow(49).GetCell(6).SetCellValue((sum + Utilities.GetVAT(sum)).ToString("0.00"));
            // sheet1.GetRow(50).GetCell(6).SetCellValue(Utilities.GetVATRub(sum).ToString("0.00"));

            // sheet1.GetRow(44).GetCell(6).SetCellValue((sum + Utilities.GetVAT(sum)).ToString("0.00"));
            // sheet1.GetRow(45).GetCell(6).SetCellValue((sum + Utilities.GetVAT(sum)).ToString("0.00"));
            //  sheet1.GetRow(46).GetCell(6).SetCellValue((sum + Utilities.GetVAT(sum)).ToString("0.00"));
            //  sheet1.GetRow(47).GetCell(6).SetCellValue((sum + Utilities.GetVAT(sum)).ToString("0.00"));
            //  sheet1.GetRow(48).GetCell(6).SetCellValue((sum + Utilities.GetVAT(sum)).ToString("0.00"));
            sheet1.GetRow(51).GetCell(6).SetCellValue(price.ToString("0.00"));
            /* sheet1.GetRow(2).GetCell(1).SetCellValue(300);
             sheet1.GetRow(3).GetCell(1).SetCellValue(500050);
             sheet1.GetRow(4).GetCell(1).SetCellValue(8000);
             sheet1.GetRow(5).GetCell(1).SetCellValue(110);
             sheet1.GetRow(6).GetCell(1).SetCellValue(100);
             sheet1.GetRow(7).GetCell(1).SetCellValue(200);
             sheet1.GetRow(8).GetCell(1).SetCellValue(210);
             sheet1.GetRow(9).GetCell(1).SetCellValue(2300);
             sheet1.GetRow(10).GetCell(1).SetCellValue(240);
             sheet1.GetRow(11).GetCell(1).SetCellValue(180123);
             sheet1.GetRow(12).GetCell(1).SetCellValue(150);*/

            sheet1.GetRow(23).GetCell(GetLetterNumber("a")).SetCellValue("Сумма прописью: " + RuDateAndMoneyConverter.CurrencyToTxtRub(price, true));
            sheet1.GetRow(53).GetCell(GetLetterNumber("a")).SetCellValue("Сумма прописью: " + RuDateAndMoneyConverter.CurrencyToTxtRub(price, true));

            //Force excel to recalculate all the formula while open
            sheet1.ForceFormulaRecalculation = true;

            WriteToFileUARublesForLittleWDK();
        }

       /* public static void GenerateWithdrawalActSpecial(int id)
        {
            UniversalEntity ue = new UniversalEntity();
            string header = string.Empty;
            string actNumber = string.Empty;
            string actNumber2 = string.Empty;
            string footer = string.Empty;

            WAbonentDO faDO = new WAbonentDO();

            ue = faDO.RetrieveBySOrderID(id);
            if (ue.Count > 0)
            {
                WAbonent fa = (WAbonent)ue[0];
                header += fa.Surname + " " + fa.FirstName + " " + fa.LastName + " \nадрес: " + fa.Address + "\nтел.: " + fa.Phone;
            }

            WOrder fo = new WOrder();
            WOrderDO foDO = new WOrderDO();
            WOrderDetailsDO fodDO = new WOrderDetailsDO();

            ue = foDO.RetrieveSOrderById(id);
            if (ue.Count > 0)
            {
                fo = (WOrder)ue[0];
                actNumber = "  г. Донецк            АКТ № " + fo.Prefix + fo.ID + " от _________________________________г.";
                actNumber2 = "                             по договору (письму) №  " + fo.Prefix + fo.ID + " от " + RuDateAndMoneyConverter.DateToTextLong(fo.DateIn) + " г.";
            }

            ue = faDO.RetrieveBySOrderID(id);
            if (ue.Count > 0)
            {
                WAbonent fa = (WAbonent)ue[0];
                footer += " Исполнитель:__________________Ковальчук С.П. " + "                                           Заказчик:_______________" + fa.Surname + " " + fa.FirstName + " " + fa.LastName;
            }

            ue = fodDO.RetrieveSOrderDetailsBySorderID(id);

            InitializeWorkbookWithdrawalActSpecial(); //InitializeWorkbook();
            ISheet sheet1 = hssfworkbook.GetSheet("s");
            //create cell on rows, since rows do already exist,it's not necessary to create rows again.
            sheet1.GetRow(2).GetCell(3).SetCellValue(header);
            sheet1.GetRow(32).GetCell(3).SetCellValue(header);
            sheet1.GetRow(9).GetCell(0).SetCellValue(actNumber);
            sheet1.GetRow(39).GetCell(0).SetCellValue(actNumber);
            sheet1.GetRow(11).GetCell(0).SetCellValue(actNumber2);
            sheet1.GetRow(41).GetCell(0).SetCellValue(actNumber2);
            sheet1.GetRow(26).GetCell(0).SetCellValue(footer);
            sheet1.GetRow(56).GetCell(0).SetCellValue(footer);

            int rowC = 44;
            int row = 14;
            double sum = 0;
            double grn = 0;
            double getvatrub = 0;
            double sumrub = 0;
            double getvat = 0;
            double finish = 0;

            int count = ue.Count;

            if (count <= 5)
            {
                CustomRetrieverDO crDO = new CustomRetrieverDO();
                ue = crDO.RetrieveWithdrawalActBySOrderID5LowWithdrawalSpecial(id);
                if (ue.Count > 0)
                {
                    for (int i = row; i < ue.Count + row; i++)
                    {
                        ArrayList al = (ArrayList)ue[i - row];
                        //FOrderDetailsAct u = (FOrderDetailsAct)fodal[i - row];
                        /*            sumrub = sum;
            getvat = sumrub*0.2;
            getvat = Math.Round(getvat, 2);
            finish = (sumrub + getvat) + (sumrub + getvat);*/

                      /*  double price = 0;
                        double nds = 0;
                        double givememoney = 0;

                        price += Convert.ToDouble(al[3]);
                        nds = price * 0.2;
                        nds = Math.Round(nds, 2);
                        givememoney = (price + nds) + (price + nds);

                        sheet1.GetRow(i).GetCell(0).SetCellValue(i - row + 1);
                        sheet1.GetRow(i).GetCell(1).SetCellValue(string.Format("Снятие, определения метрологических характеристик, \nустановка водомера  D{0}", al[0].ToString()));
                        sheet1.GetRow(i).GetCell(2).SetCellValue("калькуляция");
                        sheet1.GetRow(i).GetCell(3).SetCellValue("шт.");
                        sheet1.GetRow(i).GetCell(4).SetCellValue(al[4].ToString());
                        sheet1.GetRow(i).GetCell(5).SetCellValue(Convert.ToDouble(al[3]).ToString("0.00"));
                        sheet1.GetRow(i).GetCell(6).SetCellValue(Convert.ToDouble(al[3]).ToString("0.00"));
                        //sheet1.GetRow(i).GetCell(5).SetCellValue(givememoney.ToString("0.00"));
                        //sheet1.GetRow(i).GetCell(6).SetCellValue(givememoney.ToString("0.00"));


                        sheet1.GetRow(rowC).GetCell(0).SetCellValue(i - row + 1);
                        sheet1.GetRow(rowC).GetCell(1).SetCellValue(string.Format("Снятие, определения метрологических характеристик,\nустановка водомера  D{0}", al[0].ToString()));
                        sheet1.GetRow(rowC).GetCell(2).SetCellValue("калькуляция");
                        sheet1.GetRow(rowC).GetCell(3).SetCellValue("шт.");
                        sheet1.GetRow(rowC).GetCell(4).SetCellValue(al[4].ToString());
                        sheet1.GetRow(rowC).GetCell(5).SetCellValue(Convert.ToDouble(al[3]).ToString("0.00"));
                        sheet1.GetRow(rowC).GetCell(6).SetCellValue(Convert.ToDouble(al[3]).ToString("0.00"));
                        // sheet1.GetRow(rowC).GetCell(5).SetCellValue(givememoney.ToString("0.00"));
                        //sheet1.GetRow(rowC).GetCell(6).SetCellValue(givememoney.ToString("0.00"));


                        sum += Convert.ToDouble(al[3]);
                        rowC++;
                    }
                }
            }
            else
            {
                CustomRetrieverDO crDO = new CustomRetrieverDO();
                ue = crDO.RetrieveWithdrawalActBySOrderID5HighWithdrawalSpecial(id);
                if (ue.Count > 0)
                {
                    for (int i = row; i < ue.Count + row; i++)
                    {
                        ArrayList al = (ArrayList)ue[i - row];
                        //FOrderDetailsAct u = (FOrderDetailsAct)fodal[i - row];

                        //рублевый счет/блок для al[1]
                        double price = 0;
                        double nds = 0;
                        double givememoney = 0;

                        price += Convert.ToDouble(al[1]);
                        nds = price * 0.2;
                        nds = Math.Round(nds, 2);
                        givememoney = (price + nds) + (price + nds);


                        //рублевый счет/блок для al[2]
                        double business = 0;
                        double makebusiness = 0;
                        double somemoney = 0;

                        business += Convert.ToDouble(al[2]);
                        makebusiness = business * 0.2;
                        makebusiness = Math.Round(makebusiness, 2);
                        somemoney = (business + makebusiness) + (business + makebusiness);


                        sheet1.GetRow(i).GetCell(0).SetCellValue(i - row + 1);
                        sheet1.GetRow(i).GetCell(1).SetCellValue(string.Format("Снятие, определения метрологических характеристик, \nустановка водомера  D{0}", al[0].ToString()));
                        sheet1.GetRow(i).GetCell(2).SetCellValue("калькуляция");
                        sheet1.GetRow(i).GetCell(3).SetCellValue("шт.");
                        sheet1.GetRow(i).GetCell(4).SetCellValue(al[3].ToString());
                        sheet1.GetRow(i).GetCell(5).SetCellValue(Convert.ToDouble(al[1]).ToString("0.00"));
                        sheet1.GetRow(i).GetCell(6).SetCellValue(Convert.ToDouble(al[2]).ToString("0.00"));


                        sheet1.GetRow(rowC).GetCell(0).SetCellValue(i - row + 1);
                        sheet1.GetRow(rowC).GetCell(1).SetCellValue(string.Format("Снятие, определения метрологических характеристик, \nустановка водомера  D{0}", al[0].ToString()));
                        sheet1.GetRow(rowC).GetCell(2).SetCellValue("калькуляция");
                        sheet1.GetRow(rowC).GetCell(3).SetCellValue("шт.");
                        sheet1.GetRow(rowC).GetCell(4).SetCellValue(al[3].ToString());
                        sheet1.GetRow(rowC).GetCell(5).SetCellValue(Convert.ToDouble(al[1]).ToString("0.00"));
                        sheet1.GetRow(rowC).GetCell(6).SetCellValue(Convert.ToDouble(al[2]).ToString("0.00"));

                        sum += Convert.ToDouble(al[2]);
                        rowC++;
                    }
                }
            }

            //рублевый счет для вывода итоговый суммы
            sumrub = sum;
            getvat = sumrub * 0.2;
            getvat = Math.Round(getvat, 2);
            finish = (sumrub + getvat) + (sumrub + getvat);
            //для вывода ндс и цены рублевых
            grn = sum * 2;
            getvatrub = grn * 0.2;

            // sheet1.GetRow(19).GetCell(6).SetCellValue(grn.ToString("0.00"));
            //sheet1.GetRow(20).GetCell(6).SetCellValue(getvatrub.ToString("0.00"));
            sheet1.GetRow(21).GetCell(6).SetCellValue(sumrub.ToString("0.00"));

            //sheet1.GetRow(49).GetCell(6).SetCellValue(grn.ToString("0.00"));
            //sheet1.GetRow(50).GetCell(6).SetCellValue(getvatrub.ToString("0.00"));
            sheet1.GetRow(51).GetCell(6).SetCellValue(sumrub.ToString("0.00"));

            //sheet1.GetRow(35).GetCell(GetLetterNumber("a")).SetCellValue("Загальна сума, що підлягає оплаті: " + UADateAndMoneyConverter.CurrencyToTxt((sum + Utilities.GetVAT(sum)), true));
            sheet1.GetRow(24).GetCell(GetLetterNumber("a")).SetCellValue("Общая сумма, подлежащая оплате: " + RuDateAndMoneyConverter.CurrencyToTxtRub(sumrub, true));
            sheet1.GetRow(54).GetCell(GetLetterNumber("a")).SetCellValue("Общая сумма, подлежащая оплате: " + RuDateAndMoneyConverter.CurrencyToTxtRub(sumrub, true));


            //Force excel to recalculate all the formula while open
            sheet1.ForceFormulaRecalculation = true;

            WriteToFileWithdrawalSpecial();
        }
    }

    public class WAbonent : UniversalEntity
    {
        #region Attributes

        int _ID;
        int _FAbonentID;
        string _FirstName;
        string _Surname;
        string _LastName;
        string _Phone;
        string _PhysicalNumberJournal;
        string _Address;
        int _DistrictID;


        #endregion

        #region Properties

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public int FAbonentID
        {
            get { return _FAbonentID; }
            set { _FAbonentID = value; }
        }

        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }

        public string Surname
        {
            get { return _Surname; }
            set { _Surname = value; }
        }

        public string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }

        public int DistrictID
        {
            get { return _DistrictID; }
            set { _DistrictID = value; }
        }

        public string PhysicalNumberJournal
        {
            get { return _PhysicalNumberJournal; }
            set { _PhysicalNumberJournal = value; }
        }

        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }

        public string Phone
        {
            get { return _Phone; }
            set { _Phone = value; }
        }

        #endregion

        #region Methods

        public WAbonent()
        {
            _ID = 0;
            _Address = "";
            _DistrictID = 0;
            _FAbonentID = 0;
            _FirstName = "";
            _Surname = "";
            _LastName = "";
            _PhysicalNumberJournal = "";
            _Phone = "";
        }

        #endregion
    }

    public class WAbonentDO : UniversalDO
    {
        void AddParametersToSqlCommand(WAbonent ent, ref SqlCommand sc)
        {
            sc.Parameters.Add("@FirstName", ent.FirstName);
            sc.Parameters.Add("@Surname", ent.Surname);
            sc.Parameters.Add("@LastName", ent.LastName);
            sc.Parameters.Add("@Phone", ent.Phone);
            sc.Parameters.Add("@Address", ent.Address);
            sc.Parameters.Add("@NumberJournal", ent.PhysicalNumberJournal);
            sc.Parameters.Add("@DistrictID", ent.DistrictID);
        }

        void addParametres(WAbonent ent)
        {
            AddParametersToSqlCommand(ent, ref sc);
        }

        public int Create(WAbonent ent)
        {
            int createid = 0;
            WAbonentDAO entDAO = new WAbonentDAO();
            sc = new SqlCommand("Create
                       * ent");
            sc.CommandType = CommandType.StoredProcedure;
            addParametres(ent);
            createid = entDAO.createEntity(sc);
            return createid;
        }

        public bool CreateSAbonentByFAbonentID(WAbonent ent, int UserID)
        {
            bool success = true;
            WAbonentDAO entDAO = new WAbonentDAO();
            sc = new SqlCommand("CreateSAbonentByFAbonentID");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@UserID", UserID);
            sc.Parameters.Add("@SAbonentID", ent.ID);
            sc.Parameters.Add("@FirstName", ent.FirstName);
            sc.Parameters.Add("@Surname", ent.Surname);
            sc.Parameters.Add("@LastName", ent.LastName);
            sc.Parameters.Add("@Phone", ent.Phone);
            sc.Parameters.Add("@NumberJournal", ent.PhysicalNumberJournal);
            sc.Parameters.Add("@DistrictID", ent.DistrictID);
            sc.Parameters.Add("@Address", ent.Address);
            success = entDAO.updateEntity(sc);
            return success;
        }

        public bool Delete(int SAbonentID, int UserID)
        {
            bool success = true;
            WAbonentDAO entDAO = new WAbonentDAO();
            sc = new SqlCommand("DeleteSAbonent");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@UserID", UserID);
            sc.Parameters.Add("@SAbonentID", SAbonentID);
            success = entDAO.updateEntity(sc);
            return success;
        }

        /* public UniversalEntity RetrieveSAbonentByOrderID(int id)
         {
             SAbonentDAO entDAO = new SAbonentDAO();
             sc = new SqlCommand("RetrieveSAbonentByOrderID");
             sc.CommandType = CommandType.StoredProcedure;
             sc.Parameters.Add("@OrderID", id);
             return (entDAO.retrieveEntity(sc));
         }*/


       /* public UniversalEntity RetrieveBySOrderID(int id)
        {
            WAbonentDAO entDAO = new WAbonentDAO();
            sc = new SqlCommand("RetrieveSAbonentByOrderID");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@OrderID", id);
            return (entDAO.retrieveEntity(sc));
        }
    }

    public class WAbonentDAO : UniversalDAO
    {
        public override UniversalEntity createEntity()
        {
            UniversalEntity ue = new UniversalEntity();

            while (dr.Read())
            {
                WAbonent ent = new WAbonent();
                ent = createEntityFromReader(dr);
                ue.Add(ent);
            }
            return ue;
        }

        public WAbonent createEntityFromReader(SqlDataReader dr)
        {
            WAbonent ent = new WAbonent();

            if (!dr.IsDBNull(dr.GetOrdinal("ID")))
                ent.ID = Convert.ToInt32(dr["ID"]);

            /*if (!dr.IsDBNull(dr.GetOrdinal("FAbonentID")))
                ent.FAbonentID = Convert.ToInt32(dr["FAbonentID"]);*/

           /* if (!dr.IsDBNull(dr.GetOrdinal("FirstName")))
                ent.FirstName = dr["FirstName"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("Surname")))
                ent.Surname = dr["Surname"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("LastName")))
                ent.LastName = dr["LastName"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("Phone")))
                ent.Phone = dr["Phone"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("NumberJournal")))
                ent.PhysicalNumberJournal = dr["NumberJournal"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("Address")))
                ent.Address = dr["Address"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("DistrictID")))
                ent.DistrictID = Convert.ToInt32(dr["DistrictID"]);

            return ent;
        }

        public UniversalEntity createEntity(SqlDataReader idr)
        {
            dr = idr;
            return createEntity();
        }
    }

    public class WOrder : UniversalEntity
    {
        #region Attributes

        int _ID;
        int _SAbonentID;
        string _WorkType;
        int _UserID;
        bool _IsPaid;
        DateTime _DateIn;
        DateTime? _DateOut;
        DateTime? _PaymentDay;
        string _Prefix;

        #endregion

        #region Properties

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public int SAbonentID
        {
            get { return _SAbonentID; }
            set { _SAbonentID = value; }
        }

        public string WorkType
        {
            get { return _WorkType; }
            set { _WorkType = value; }
        }

        public int UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }

        public bool IsPaid
        {
            get { return _IsPaid; }
            set { _IsPaid = value; }
        }

        public DateTime DateIn
        {
            get { return _DateIn; }
            set { _DateIn = value; }
        }

        public DateTime? DateOut
        {
            get { return _DateOut; }
            set { _DateOut = value; }
        }

        public DateTime? PaymentDay
        {
            get { return _PaymentDay; }
            set { _PaymentDay = value; }
        }

        public string Prefix
        {
            get { return _Prefix; }
            set { _Prefix = value; }
        }

        #endregion

        #region Methods

        public WOrder()
        {
            _ID = 0;
            _SAbonentID = 0;
            _WorkType = "Снятие/установка водомеров";
            _UserID = 0;
            _IsPaid = false;
            _DateIn = DateTime.MinValue;
            _PaymentDay = null;
            _DateOut = null;
            _Prefix = "";
        }

        #endregion
    }

    public class WOrderDO : UniversalDO
    {
        void AddParametresToSqlCommand(WOrder ent, ref SqlCommand sc)
        {
            sc.Parameters.Add("@SAbonentID", ent.SAbonentID);
            sc.Parameters.Add("@WorkType", ent.WorkType);
            sc.Parameters.Add("@UserID", ent.UserID);
        }

        void addParametres(WOrder ent)
        {
            AddParametresToSqlCommand(ent, ref sc);
        }

        public int CreateSOrder(WOrder ent)
        {
            int createid = 0;
            WOrderDAO entDAO = new WOrderDAO();
            sc = new SqlCommand("CreateSOrder");
            sc.CommandType = CommandType.StoredProcedure;
            addParametres(ent);
            createid = entDAO.createEntity(sc);
            return createid;
        }


        public bool UpdateSOrder(WOrder ent)
        {
            bool success = true;
            WOrderDAO entDAO = new WOrderDAO();
            sc = new SqlCommand("UpdateSOrder");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@ID", ent.ID);
            sc.Parameters.Add("@IsPaid", ent.IsPaid);
            sc.Parameters.Add("@UserID", ent.UserID);
            if (ent.DateOut == null)
            {
                sc.Parameters.Add("@DateOut", DBNull.Value);
            }
            else
            {
                sc.Parameters.Add("@DateOut", ent.DateOut);
            }
            if (ent.PaymentDay == null)
            {
                sc.Parameters.Add("@PaymentDay", DBNull.Value);
            }
            else
            {
                sc.Parameters.Add("@PaymentDay", ent.PaymentDay.Value);
            }
            //addParameters(ent);
            success = entDAO.updateEntity(sc);
            return success;
        }

        /* public UniversalEntity RetrieveSOrderByID(int id)
         {
             SOrderDAO entDAO = new SOrderDAO();
             sc = new SqlCommand("RetrieveSOrderById");
             sc.CommandType = CommandType.StoredProcedure;
             sc.Parameters.Add("@ID", id);
             return (entDAO.retrieveEntity(sc));
         }*/


     /*   public UniversalEntity RetrieveSOrderById(int id)
        {
            WOrderDAO entDAO = new WOrderDAO();
            sc = new SqlCommand("RetrieveSOrderById");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@ID", id);
            return (entDAO.retrieveEntity(sc));
        }
    }

    public class WOrderDAO : UniversalDAO
    {
        public override UniversalEntity createEntity()
        {
            UniversalEntity ue = new UniversalEntity();

            while (dr.Read())
            {
                WOrder ent = new WOrder();
                ent = createEntityFromReader(dr);
                ue.Add(ent);
            }

            return ue;
        }

        public WOrder createEntityFromReader(SqlDataReader dr)
        {
            WOrder ent = new WOrder();

            if (!dr.IsDBNull(dr.GetOrdinal("ID")))
                ent.ID = Convert.ToInt32(dr["ID"]);

            if (!dr.IsDBNull(dr.GetOrdinal("SAbonentID")))
                ent.SAbonentID = Convert.ToInt32(dr["SAbonentID"]);

            if (!dr.IsDBNull(dr.GetOrdinal("WorkType")))
                ent.WorkType = dr["WorkType"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("UserID")))
                ent.UserID = Convert.ToInt32(dr["UserID"]);

            if (!dr.IsDBNull(dr.GetOrdinal("DateIn")))
                ent.DateIn = Convert.ToDateTime(dr["DateIn"]);

            if (!dr.IsDBNull(dr.GetOrdinal("DateOut")))
                ent.DateOut = Convert.ToDateTime(dr["DateOut"]);

            if (!dr.IsDBNull(dr.GetOrdinal("PaymentDay")))
                ent.PaymentDay = Convert.ToDateTime(dr["PaymentDay"]);

            if (!dr.IsDBNull(dr.GetOrdinal("Prefix")))
                ent.Prefix = dr["Prefix"].ToString();

            return ent;
        }

        public UniversalEntity createEntity(SqlDataReader idr)
        {
            dr = idr;
            return createEntity();
        }
    }

    public class WOrderDetails : UniversalEntity
    {
        #region Attributes

        int _ID;
        int _SOrderID;
        int _VodomerID;
        string _StartValue;
        string _EndValue;
        double _SpecialPrice;

        #endregion

        #region Properties

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public int SOrderID
        {
            get { return _SOrderID; }
            set { _SOrderID = value; }
        }

        public int VodomerID
        {
            get { return _VodomerID; }
            set { _VodomerID = value; }
        }

        public string StartValue
        {
            get { return _StartValue; }
            set { _StartValue = value; }
        }

        public string EndValue
        {
            get { return _EndValue; }
            set { _EndValue = value; }
        }

        public double SpecialPrice
        {
            get { return _SpecialPrice; }
            set { _SpecialPrice = value; }
        }

        #endregion

        #region Methods

        public WOrderDetails()
        {
            _ID = 0;
            _SOrderID = 0;
            _VodomerID = 0;
            _StartValue = "";
            _EndValue = "";
            _SpecialPrice = 0;
        }

        #endregion
    }

    public class WOrderDetailsDO : UniversalDO
    {
        void AddParametresToSqlCommand(WOrderDetails ent, ref SqlCommand sc)
        {
            sc.Parameters.Add("@SOrderID", ent.SOrderID);
            sc.Parameters.Add("@VodomerID", ent.VodomerID);
            sc.Parameters.Add("@StartValue", ent.StartValue);
            // sc.Parameters.Add("@EndValue", ent.EndValue);
            // sc.Parameters.Add("@SpecialPrice", ent.SpecialPrice);
        }

        void addParametres(WOrderDetails ent)
        {
            AddParametresToSqlCommand(ent, ref sc);
        }

        public int Create(WOrderDetails ent)
        {
            int createid = 0;
            WOrderDetailsDAO entDAO = new WOrderDetailsDAO();
            sc = new SqlCommand("CreateSOrderDetails");
            sc.CommandType = CommandType.StoredProcedure;
            addParametres(ent);
            createid = entDAO.createEntity(sc);
            return createid;
        }

        public UniversalEntity VodomersBySOrder(int id)
        {
            WOrderDetailsDAO entDAO = new WOrderDetailsDAO();
            sc = new SqlCommand("VodomersBySOrder");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@SOrderID", id);
            return (entDAO.retrieveEntity(sc));
        }

        public UniversalEntity RetrieveSOrderDetailsBySorderID(int id)
        {
            WOrderDetailsDAO entDAO = new WOrderDetailsDAO();
            sc = new SqlCommand("RetrieveSOrderDetailsByOrderID");
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@OrderID", id);
            return (entDAO.retrieveEntity(sc));
        }
    }

    public class WOrderDetailsDAO : UniversalDAO
    {
        public override UniversalEntity createEntity()
        {
            UniversalEntity ue = new UniversalEntity();

            while (dr.Read())
            {
                WOrderDetails ent = new WOrderDetails();
                ent = createEntityFromReader(dr);
                ue.Add(ent);
            }
            return ue;
        }

        public WOrderDetails createEntityFromReader(SqlDataReader dr)
        {
            WOrderDetails ent = new WOrderDetails();

            if (!dr.IsDBNull(dr.GetOrdinal("ID")))
                ent.ID = Convert.ToInt32(dr["ID"]);

            if (!dr.IsDBNull(dr.GetOrdinal("SOrderID")))
                ent.SOrderID = Convert.ToInt32(dr["SOrderID"]);

            if (!dr.IsDBNull(dr.GetOrdinal("VodomerID")))
                ent.VodomerID = Convert.ToInt32(dr["VodomerID"]);

            if (!dr.IsDBNull(dr.GetOrdinal("StartValue")))
                ent.StartValue = dr["StartValue"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("EndValue")))
                ent.EndValue = dr["EndValue"].ToString();

            if (!dr.IsDBNull(dr.GetOrdinal("SpecialPrice")))
                ent.SpecialPrice = Convert.ToDouble(dr["SpecialPrice"]);

            return ent;
        }

        public UniversalEntity createEntity(SqlDataReader idr)
        {
            dr = idr;
            return createEntity();
        }*/
    }
}