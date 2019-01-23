using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.HPSF;
using NPOI.POIFS.FileSystem;
using NPOI.SS.UserModel;
using Entities;
using DomainObjects;

namespace kipia_web_application
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
                    actNumber = "  г. Донецк            АКТ № " + uo.ID + " от _________________________________г.";
                }
                else
                {
                    actNumber = "  г. Донецк            АКТ № " + uo.ID + " от _________________________________ г.";
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
                actNumber = "  г. Донецк            АКТ № " + fo.Prefix + fo.ID + " от _________________________________г.";
                actNumber2 = "                             по договору (письму) №  " + fo.Prefix + fo.ID + " от " + RuDateAndMoneyConverter.DateToTextLong(fo.DateIn) + " г.";
            }

            ue = faDO.RetrieveByOrderID(id);
            if (ue.Count > 0)
            {
                FAbonent fa = (FAbonent) ue[0];
                footer += " Исполнитель:__________________Ковальчук С.П. " + "                                           Заказчик:_______________" + fa.Surname + " " + fa.FirstName + " " + fa.LastName;
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
            sheet1.GetRow(26).GetCell(0).SetCellValue(footer);
            sheet1.GetRow(56).GetCell(0).SetCellValue(footer);

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
            sheet1.GetRow(24).GetCell(GetLetterNumber("a")).SetCellValue("Общая сумма, подлежащая оплате: " + RuDateAndMoneyConverter.CurrencyToTxt((sum + Utilities.GetVAT(sum)), true));
            sheet1.GetRow(54).GetCell(GetLetterNumber("a")).SetCellValue("Общая сумма. подлежащая оплате: " + RuDateAndMoneyConverter.CurrencyToTxt((sum + Utilities.GetVAT(sum)), true));

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
                actNumber = "  г. Донецк            АКТ № " + fo.Prefix + fo.ID + " от _________________________________ г.";
                actNumber2 = "                             по договору (письму) №  " + fo.Prefix + fo.ID + " от " + RuDateAndMoneyConverter.DateToTextLong(fo.DateIn) + " г.";
            }

            ue = faDO.RetrieveByOrderID(id);
            if (ue.Count > 0)
            {
                fa = (FAbonent) ue[0];
                footer += " Исполнитель:__________________Ковальчук С.П. " + "                                           Заказчик:_______________" + fa.Surname + " " + fa.FirstName + " " + fa.LastName;
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

            sheet1.GetRow(24).GetCell(GetLetterNumber("a")).SetCellValue("Общая сумма, подлежащая оплате: " + RuDateAndMoneyConverter.CurrencyToTxt((sum + Utilities.GetVAT(sum)), true));

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

                        sheet1.GetRow(i).GetCell(GetLetterNumber("a")).SetCellValue(string.Format("Определения метрологических характеристик водомера  D{0}", al[0].ToString()));
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

                        sheet1.GetRow(i).GetCell(GetLetterNumber("a")).SetCellValue(string.Format("Определения метрологических характеристик водомера  D{0}", al[0].ToString()));
                        sheet1.GetRow(i).GetCell(GetLetterNumber("ad")).SetCellValue("шт.");
                        sheet1.GetRow(i).GetCell(GetLetterNumber("aj")).SetCellValue(al[3].ToString());
                        sheet1.GetRow(i).GetCell(GetLetterNumber("ap")).SetCellValue(Convert.ToDouble(al[3]).ToString("0.00"));
                        sheet1.GetRow(i).GetCell(GetLetterNumber("av")).SetCellValue(Convert.ToDouble(al[3]).ToString("0.00"));

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

        static void WriteToFileRubF()
        {
            string path = HttpContext.Current.Request.MapPath("~\\Templates/RubFact.xls");
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
        static void WriteToFileFSpecial()
        {
            //Write the stream data of workbook to the root directory
            string path = HttpContext.Current.Request.MapPath("~\\Templates/out act_check.xls");
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
                actNumber = "  г. Донецк            АКТ № " + fo.Prefix + fo.ID + " от _________________________________г.";
                actNumber2 = "                             по договору (письму) №  " + fo.Prefix + fo.ID + " от " + RuDateAndMoneyConverter.DateToTextLong(fo.DateIn) + " г.";
            }

            ue = faDO.RetrieveByOrderID(id);
            if (ue.Count > 0)
            {
                FAbonent fa = (FAbonent) ue[0];
                footer += " Исполнитель:__________________Ковальчук С.П. " + "                                           Заказчик:_______________" + fa.Surname + " " + fa.FirstName + " " + fa.LastName;
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
                        nds = price*0.2;
                        nds = Math.Round(nds, 2);
                        givememoney = (price + nds) + (price + nds);

                        sheet1.GetRow(i).GetCell(0).SetCellValue(i - row + 1);
                        sheet1.GetRow(i).GetCell(1).SetCellValue(string.Format("Определения метрологических характеристик водомера  D{0}", al[0].ToString()));
                        sheet1.GetRow(i).GetCell(2).SetCellValue("калькуляция");
                        sheet1.GetRow(i).GetCell(3).SetCellValue("шт.");
                        sheet1.GetRow(i).GetCell(4).SetCellValue(al[4].ToString());
                        sheet1.GetRow(i).GetCell(7).SetCellValue(Convert.ToDouble(al[3]).ToString("0.00"));
                        sheet1.GetRow(i).GetCell(8).SetCellValue(Convert.ToDouble(al[3]).ToString("0.00"));
                        sheet1.GetRow(i).GetCell(5).SetCellValue(givememoney.ToString("0.00"));
                        sheet1.GetRow(i).GetCell(6).SetCellValue(givememoney.ToString("0.00"));


                        sheet1.GetRow(rowC).GetCell(0).SetCellValue(i - row + 1);
                        sheet1.GetRow(rowC).GetCell(1).SetCellValue(string.Format("Определения метрологических характеристик водомера  D{0}", al[0].ToString()));
                        sheet1.GetRow(rowC).GetCell(2).SetCellValue("калькуляция");
                        sheet1.GetRow(rowC).GetCell(3).SetCellValue("шт.");
                        sheet1.GetRow(rowC).GetCell(4).SetCellValue(al[4].ToString());
                        sheet1.GetRow(rowC).GetCell(7).SetCellValue(Convert.ToDouble(al[3]).ToString("0.00"));
                        sheet1.GetRow(rowC).GetCell(8).SetCellValue(Convert.ToDouble(al[3]).ToString("0.00"));
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
            sheet1.GetRow(21).GetCell(6).SetCellValue(finish.ToString("0.00"));

            //sheet1.GetRow(49).GetCell(6).SetCellValue(grn.ToString("0.00"));
            //sheet1.GetRow(50).GetCell(6).SetCellValue(getvatrub.ToString("0.00"));
            sheet1.GetRow(51).GetCell(6).SetCellValue(finish.ToString("0.00"));

            //sheet1.GetRow(35).GetCell(GetLetterNumber("a")).SetCellValue("Загальна сума, що підлягає оплаті: " + UADateAndMoneyConverter.CurrencyToTxt((sum + Utilities.GetVAT(sum)), true));
            sheet1.GetRow(24).GetCell(GetLetterNumber("a")).SetCellValue("Общая сумма, подлежащая оплате: " + RuDateAndMoneyConverter.CurrencyToTxtRub(finish, true));
            sheet1.GetRow(54).GetCell(GetLetterNumber("a")).SetCellValue("Общая сумма, подлежащая оплате: " + RuDateAndMoneyConverter.CurrencyToTxtRub(finish, true));


            //Force excel to recalculate all the formula while open
            sheet1.ForceFormulaRecalculation = true;

            WriteToFileRubF();
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
                actNumber = "  г. Донецк            АКТ № " + fo.Prefix + fo.ID + " от _________________________________ г.";
                actNumber2 = "                             по договору (письму) №  " + fo.Prefix + fo.ID + " от " + RuDateAndMoneyConverter.DateToTextLong(fo.DateIn) + " г.";
            }

            ue = faDO.RetrieveByOrderID(id);
            if (ue.Count > 0)
            {
                fa = (FAbonent) ue[0];
                footer += " Исполнитель:__________________Ковальчук С.П. " + "                                           Заказчик:_______________" + fa.Surname + " " + fa.FirstName + " " + fa.LastName;
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
                        sheet1.GetRow(i).GetCell(7).SetCellValue(Convert.ToDouble(al[3]).ToString("0.00"));
                        sheet1.GetRow(i).GetCell(8).SetCellValue(Convert.ToDouble(al[3]).ToString("0.00"));
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
            sheet1.GetRow(21).GetCell(6).SetCellValue((sum + (sum / 100 * 20)).ToString("0.00"));

            //sheet1.GetRow(24).GetCell(GetLetterNumber("a")).SetCellValue("Общая сумма, подлежащая оплате: " + RuDateAndMoneyConverter.CurrencyToTxtRub(finish, true));

           //рублевый счет
            sumrub = sum;
            getvat = sumrub*0.2;
            getvat = Math.Round(getvat, 2);
            finish = (sumrub + getvat) + (sumrub + getvat);

            sheet1.GetRow(24).GetCell(GetLetterNumber("a")).SetCellValue("Общая сумма, подлежащая оплате: " + RuDateAndMoneyConverter.CurrencyToTxtRub(finish, true));

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
            //tmp = tmp.Replace("SUM", sum.ToString("0.00"));
            //tmp = tmp.Replace("VAT", Utilities.GetVAT(sum).ToString("0.00"));
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

            WriteToFileFSpecialRub();
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
                        getfin = (getnds + sumone);
                       
                        sheet1.GetRow(i).GetCell(GetLetterNumber("a")).SetCellValue(string.Format("Определения метрологических характеристик водомера  D{0}", al[0].ToString()));
                        sheet1.GetRow(i).GetCell(GetLetterNumber("ad")).SetCellValue("шт.");
                        sheet1.GetRow(i).GetCell(GetLetterNumber("aj")).SetCellValue(al[4].ToString());
                        sheet1.GetRow(i).GetCell(GetLetterNumber("ap")).SetCellValue(getfin.ToString("0.00"));
                        sheet1.GetRow(i).GetCell(GetLetterNumber("av")).SetCellValue(getfin.ToString("0.00"));
                        sheet1.GetRow(i).GetCell(GetLetterNumber("az")).SetCellValue(Convert.ToDouble(al[3]));
                        sheet1.GetRow(i).GetCell(GetLetterNumber("ay")).SetCellValue(Convert.ToDouble(al[3]));
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

                        sumone += Convert.ToDouble(al[3]);
                        getnds = sumone * 0.2;
                        getnds = Math.Round(getnds, 2);
                        getfin = (getnds + sumone);

                        sheet1.GetRow(i).GetCell(GetLetterNumber("a")).SetCellValue(string.Format("Определения метрологических характеристик водомера  D{0}", al[0].ToString()));
                        sheet1.GetRow(i).GetCell(GetLetterNumber("ad")).SetCellValue("шт.");
                        sheet1.GetRow(i).GetCell(GetLetterNumber("aj")).SetCellValue(al[3].ToString());
                        sheet1.GetRow(i).GetCell(GetLetterNumber("av")).SetCellValue(getfin.ToString("0.00"));
                        sheet1.GetRow(i).GetCell(GetLetterNumber("az")).SetCellValue(Convert.ToDouble(al[3]));
                        sheet1.GetRow(i).GetCell(GetLetterNumber("ay")).SetCellValue(Convert.ToDouble(al[3]));

                        sum += Convert.ToDouble(al[3]);
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
            sheet1.GetRow(31).GetCell(GetLetterNumber("av")).SetCellValue((sum + Utilities.GetVAT(sum)).ToString("0.00"));
            //sumrub = (sum)*2;
            //sheet1.GetRow(68).GetCell(GetLetterNumber("av")).SetCellValue(((sumrub)*2).ToString("0.00"));
            sheet1.GetRow(32).GetCell(GetLetterNumber("av")).SetCellValue(finish.ToString("0.00"));
            
            sheet1.GetRow(35).GetCell(GetLetterNumber("a")).SetCellValue("Общая сумма, подлежащая оплате: " + RuDateAndMoneyConverter.CurrencyToTxt((sum + Utilities.GetVAT(sum)), true));

            
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
                    actNumber = "  г. Донецк            АКТ № " + uo.ID + " от _________________________________г.";
                }
                else
                {
                    actNumber = "  г. Донецк            АКТ № " + uo.ID + " от _________________________________ г.";
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
                        getfin = getnds + sumone;

                        sheet1.GetRow(i).GetCell(0).SetCellValue(i - row + 1);
                        sheet1.GetRow(i).GetCell(1).SetCellValue(string.Format("Определения метрологических характеристик водомера D{0}", al[0].ToString()));
                        sheet1.GetRow(i).GetCell(2).SetCellValue("калькуляция");
                        sheet1.GetRow(i).GetCell(3).SetCellValue("шт.");
                        sheet1.GetRow(i).GetCell(4).SetCellValue(al[4].ToString());
                        sheet1.GetRow(i).GetCell(7).SetCellValue(Convert.ToDouble(al[3]).ToString("0.00"));
                        sheet1.GetRow(i).GetCell(8).SetCellValue(Convert.ToDouble(al[3]).ToString("0.00"));
                        sheet1.GetRow(i).GetCell(5).SetCellValue(getfin.ToString("0.00"));
                        sheet1.GetRow(i).GetCell(6).SetCellValue(getfin.ToString("0.00"));

                        sheet1.GetRow(rowC).GetCell(0).SetCellValue(i - row + 1);
                        sheet1.GetRow(rowC).GetCell(1).SetCellValue(string.Format("Определения метрологических характеристик водомера  D{0}", al[0].ToString()));
                        sheet1.GetRow(rowC).GetCell(2).SetCellValue("калькуляция");
                        sheet1.GetRow(rowC).GetCell(3).SetCellValue("шт.");
                        sheet1.GetRow(rowC).GetCell(4).SetCellValue(al[4].ToString());
                        sheet1.GetRow(rowC).GetCell(7).SetCellValue(Convert.ToDouble(al[3]).ToString("0.00"));
                        sheet1.GetRow(rowC).GetCell(8).SetCellValue(Convert.ToDouble(al[3]).ToString("0.00"));
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
                        sheet1.GetRow(i).GetCell(5).SetCellValue(Convert.ToDouble(al[1]).ToString("0.00"));
                        sheet1.GetRow(i).GetCell(6).SetCellValue(Convert.ToDouble(al[2]).ToString("0.00"));

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

           // sheet1.GetRow(19).GetCell(6).SetCellValue((sum + Utilities.GetVAT(sum)).ToString("0.00"));
           // sheet1.GetRow(20).GetCell(6).SetCellValue(Utilities.GetVATRub(sum).ToString("0.00"));
            
           // sheet1.GetRow(14).GetCell(6).SetCellValue((sum + Utilities.GetVAT(sum)).ToString("0.00"));
          //  sheet1.GetRow(15).GetCell(6).SetCellValue((sum + Utilities.GetVAT(sum)).ToString("0.00"));
          //  sheet1.GetRow(16).GetCell(6).SetCellValue((sum + Utilities.GetVAT(sum)).ToString("0.00"));
          //  sheet1.GetRow(17).GetCell(6).SetCellValue((sum + Utilities.GetVAT(sum)).ToString("0.00"));
           // sheet1.GetRow(18).GetCell(6).SetCellValue((sum + Utilities.GetVAT(sum)).ToString("0.00"));
            sheet1.GetRow(21).GetCell(6).SetCellValue((sum + Utilities.GetVAT(sum)).ToString("0.00"));

           // sheet1.GetRow(49).GetCell(6).SetCellValue((sum + Utilities.GetVAT(sum)).ToString("0.00"));
           // sheet1.GetRow(50).GetCell(6).SetCellValue(Utilities.GetVATRub(sum).ToString("0.00"));
            
           // sheet1.GetRow(44).GetCell(6).SetCellValue((sum + Utilities.GetVAT(sum)).ToString("0.00"));
           // sheet1.GetRow(45).GetCell(6).SetCellValue((sum + Utilities.GetVAT(sum)).ToString("0.00"));
          //  sheet1.GetRow(46).GetCell(6).SetCellValue((sum + Utilities.GetVAT(sum)).ToString("0.00"));
          //  sheet1.GetRow(47).GetCell(6).SetCellValue((sum + Utilities.GetVAT(sum)).ToString("0.00"));
          //  sheet1.GetRow(48).GetCell(6).SetCellValue((sum + Utilities.GetVAT(sum)).ToString("0.00"));
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

            WriteToFileUARub();
        }
    }
}