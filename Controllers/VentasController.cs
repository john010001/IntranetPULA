using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Intranet.Models.Data;
using Intranet.ModelsApp;
using EntityFramework.BulkInsert.Extensions;
using Intranet.Helper;

namespace Intranet.Controllers
{
    [Authorize]
    public class VentasController : Controller
    {
        
        IntranetDBEntities db = new IntranetDBEntities();
        // GET: Ventas
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Importar()
        {            

            return View(db.VentasKiuAdminFile.OrderByDescending(o => o.UserFec));

        }
        [HttpPost]
        public ActionResult Importar(HttpPostedFileBase postedFile, string btnOption)
        {
            const int batchSave = 500;
            string filePath = string.Empty;

            List<TMPVentasKiuAdmin> TMPventasKiu = new List<TMPVentasKiuAdmin>();

            string path = Server.MapPath("~/Uploads/");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            filePath = path + Path.GetFileName(postedFile.FileName);
            string extension = Path.GetExtension(postedFile.FileName);
            string filename = Path.GetFileName(postedFile.FileName);


            var userClaims = User.Identity as System.Security.Claims.ClaimsIdentity;

            //You get the user’s first and last name below:
            var usuario = userClaims.FindFirst("preferred_username").Value;


            postedFile.SaveAs(filePath);

            string csvData = System.IO.File.ReadAllText(filePath, Encoding.UTF8);

            string[] arraySale;

            bool swFile = false;
                 

            foreach (string row in csvData.Split('\n'))
            {


                if (!string.IsNullOrEmpty(row))
                {
                    try
                    {
                        arraySale = row.Split(';');

                        if (swFile == false)
                        {
                            if (arraySale.Length != 280 && arraySale.Length != 279)
                            {
                                ViewBag.Error = "Estructura de Archivo incorrecta";
                                return View(db.VentasKiuAdminFile.OrderByDescending(o => o.UserFec));
                            }

                            if (db.VentasKiuAdminFile.Where(w => w.Nombre == filename).Count() > 0)
                            {
                                ViewBag.Error = "Archivo cargado anteriormente";
                                return View(db.VentasKiuAdminFile.OrderByDescending(o => o.UserFec));
                            }
                            else
                            {
                                VentasKiuAdminFile ventasKiuAdminFile = new VentasKiuAdminFile();
                                ventasKiuAdminFile.Nombre = filename;
                                ventasKiuAdminFile.UserFec = DateTime.Now;
                                ventasKiuAdminFile.NomUser = usuario;
                                ventasKiuAdminFile.Estado = 0; //Validado
                                ventasKiuAdminFile.Procesado = 0;
                                ventasKiuAdminFile.Nuevo = 0;
                                ventasKiuAdminFile.Actualizado = 0;

                                db.VentasKiuAdminFile.Add(ventasKiuAdminFile);

                                db.SaveChanges();
                            }

                            swFile = true;
                        }
                        #region AsignarValores

                       
                        TMPventasKiu.Add(new TMPVentasKiuAdmin
                        {
                            Archivo = filename,
                            Reporte = arraySale[0],
                            FReporte = arraySale[1],
                            Moneda = arraySale[2],
                            Cambio = arraySale[3],
                            Tipo_Emisor = arraySale[4],
                            Emisor = arraySale[5],
                            Agente_Reserva = arraySale[6],
                            Agente_Emisor = arraySale[7],
                            Zona = arraySale[8],
                            Pais = arraySale[9],
                            Estado = arraySale[10],
                            Localidad = arraySale[11],
                            Nro_Ticket = arraySale[12],
                            Date = arraySale[13],
                            Transac = arraySale[14],
                            Type = arraySale[15],
                            Pax = arraySale[16],
                            Pax_Type = arraySale[17],
                            FOID = arraySale[18],
                            Clave_Fiscal = arraySale[19],
                            Frequent_Flyer = arraySale[20],
                            Indicador_Fiscal = arraySale[21],
                            Cod_Reserva = arraySale[22],
                            Endoso = arraySale[23],
                            Fare_Calc = arraySale[24],
                            Tour_Code = arraySale[25],
                            Related_Doc = arraySale[26],
                            FBasisCupon1 = arraySale[27],
                            MonedaQ1 = arraySale[28],
                            ImporteQ1 = arraySale[29],
                            FBasisCupon2 = arraySale[30],
                            MonedaQ2 = arraySale[31],
                            ImporteQ2 = arraySale[32],
                            FBasisCupon3 = arraySale[33],
                            MonedaQ3 = arraySale[34],
                            ImporteQ3 = arraySale[35],
                            FBasisCupon4 = arraySale[36],
                            MonedaQ4 = arraySale[37],
                            ImporteQ4 = arraySale[38],
                            interviniente = arraySale[39],
                            OfficeId_Reserva = arraySale[40],
                            OfficeId_Emisor = arraySale[41],
                            Host = arraySale[42],
                            tipo_pago = arraySale[43],
                            C_C_CO__COMISION_ = arraySale[44],
                            C_C_IVM__Iva_de_Comision = arraySale[45],
                            C_C_VETOC__Ret_de_IVA_de_comision = arraySale[46],
                            C_C_INTVAT__INTVAT = arraySale[47],
                            C_C_PEN__IVA_COMISION = arraySale[48],
                            C_C_IVA__IVA_COMISION = arraySale[49],
                            C_C_EC1__IVA_ECUADOR = arraySale[50],
                            C_C_EC__IVA_ECUADOR = arraySale[51],
                            C_C_PE__IVA_PERU = arraySale[52],

                            // *************************
                            //Agregada el 03/09/2021
                            VATME = arraySale[53],
                            // *************************



                            C_O_OBT01__Fee_de_Emision = arraySale[54],
                            C_O_OB__PENALIZACION_CAMBIOS = arraySale[55],
                            C_O_OBT02__Fee_de_Emision = arraySale[56],
                            C_P_CA__CASH = arraySale[57],
                            Ref = arraySale[58],
                            C_P_EX__EXCHANGE = arraySale[59],
                            Ref2 = arraySale[60],
                            C_P_CCVI__Visa = arraySale[61],
                            Ref3 = arraySale[62],
                            C_P_CCIK__Mastercard = arraySale[63],
                            Ref4 = arraySale[64],
                            C_P_DCMA__Maestro = arraySale[65],
                            Ref5 = arraySale[66],
                            C_P_CCAX__American_Express = arraySale[67],
                            Ref6 = arraySale[68],
                            C_P_IN__Invoice = arraySale[69],
                            Ref7 = arraySale[70],
                            C_P_XXSERV__Free = arraySale[71],
                            Ref8 = arraySale[72],
                            C_P_XX__Free = arraySale[73],
                            Ref9 = arraySale[74],
                            C_P_XX_SER__Free = arraySale[75],
                            Ref10 = arraySale[76],
                            C_P_MSTO__Miscelaneo = arraySale[77],
                            Ref11 = arraySale[78],
                            C_P_MSGC__Miscelaneo = arraySale[79],
                            Ref12 = arraySale[80],
                            C_P_XXTKTS__Free = arraySale[81],
                            Ref13 = arraySale[82],
                            C_P_XXTKT__Freee = arraySale[83],
                            Ref14 = arraySale[84],
                            C_P_XXTKTG__Free = arraySale[85],
                            Ref15 = arraySale[86],
                            C_P_XXFREE__Free = arraySale[87],
                            Ref16 = arraySale[88],
                            C_P_XX_FRE__Free = arraySale[89],
                            Ref17 = arraySale[90],
                            C_P_CCDS__Discovery = arraySale[91],
                            Ref18 = arraySale[92],
                            C_P_MSVC__Miscelaneo = arraySale[93],
                            Ref19 = arraySale[94],
                            C_P_CCDC__Diners_Club = arraySale[95],
                            Ref20 = arraySale[96],
                            C_P_MSMI__Miscelaneo = arraySale[97],
                            Ref21 = arraySale[98],
                            C_P_MSGC___Miscelaneo = arraySale[99],
                            Ref22 = arraySale[100],
                            C_P_XX_PRE__Free = arraySale[101],
                            Ref23 = arraySale[102],
                            C_P_XXCOOR__Free = arraySale[103],
                            Ref24 = arraySale[104],
                            C_P_XXAPP__Free = arraySale[105],
                            Ref25 = arraySale[106],
                            C_P_XXGRUP__Free = arraySale[107],
                            Ref26 = arraySale[108],
                            C_P_XXTKTF__Free = arraySale[109],
                            Ref27 = arraySale[110],
                            C_P_CCCA__Mastercard = arraySale[111],
                            Ref28 = arraySale[112],
                            C_P_MSPFNV__MISCELLANEOS = arraySale[113],
                            Ref29 = arraySale[114],
                            C_P_MSPF__MSPF = arraySale[115],
                            Ref30 = arraySale[116],
                            C_P_MSTR__MSTR = arraySale[117],
                            Ref31 = arraySale[118],
                            C_P_XXTRAN__XXTRAN = arraySale[119],
                            Ref32 = arraySale[120],
                            C_P_XXTR__XXTR = arraySale[121],
                            Ref33 = arraySale[122],
                            C_P_MSTR_C__MSTR_C = arraySale[123],
                            Ref34 = arraySale[124],
                            C_P_MSTR___MSTR_ = arraySale[125],
                            Ref35 = arraySale[126],
                            C_P_MSTRTR__MSTRTR = arraySale[127],
                            Ref36 = arraySale[128],
                            C_P_MSTR_D__MSTR_D = arraySale[129],
                            Ref37 = arraySale[130],
                            C_P_MSTR_T__MS_TRANSF = arraySale[131],
                            Ref38 = arraySale[132],
                            C_P_XXCRED__XXCRED = arraySale[133],
                            Ref39 = arraySale[134],
                            C_P_MSPF_N__Pago_Facil = arraySale[135],
                            Ref40 = arraySale[136],
                            C_P_XXFMS__PAGO = arraySale[137],
                            Ref41 = arraySale[138],
                            C_P_MSPFPV__PAGO = arraySale[139],
                            Ref42 = arraySale[140],
                            C_P_XXDEPO__PAGO_DEPOSITO = arraySale[141],
                            Ref43 = arraySale[142],
                            C_P_MSTRDE__TRANSFERENCIA = arraySale[143],
                            Ref44 = arraySale[144],
                            C_P_DC__Debit_card = arraySale[145],
                            Ref45 = arraySale[146],
                            C_P_MSTRS__Transferencia = arraySale[147],
                            Ref46 = arraySale[148],
                            C_P_MSTRK__Trasnferencia = arraySale[149],
                            Ref47 = arraySale[150],
                            C_P_MSTRA__TRANSFERENCIA = arraySale[151],
                            Ref48 = arraySale[152],
                            C_P_MSTR_B__transferencia = arraySale[153],
                            Ref49 = arraySale[154],
                            C_P_CCVIXX__CCVIXX = arraySale[155],
                            Ref50 = arraySale[156],
                            C_P_CK__cheque = arraySale[157],
                            Ref51 = arraySale[158],
                            C_P_MSCC__CREDIT_CARD_OFFLINE = arraySale[159],
                            Ref52 = arraySale[160],
                            C_P_CC__CREDIT_CARD_OFFLINE = arraySale[161],
                            Ref53 = arraySale[162],
                            C_P_CCTP__UATP = arraySale[163],
                            Ref54 = arraySale[164],
                            C_P_MSCCVI__Credit_Card_Exchange = arraySale[165],
                            Ref55 = arraySale[166],
                            C_P_MSEX__EMD_Cr__dito = arraySale[167],
                            Ref56 = arraySale[168],
                            C_P_MS__EMD_COVID19 = arraySale[169],
                            Ref57 = arraySale[170],
                            C_P_CHBC__Chargebacks = arraySale[171],
                            Ref58 = arraySale[172],
                            C_P_ACM__ACM = arraySale[173],
                            Ref59 = arraySale[174],

                            //**********************************************
                            //GIFT CARD-- AMAZON PAY
                            TAC_P_MSGP__GIFTCARD_PREMIERPLUS = arraySale[175],
                            RefGC = arraySale[176],
                            C_P_MSAP__AMAZON_PAYMENT = arraySale[177],
                            RefAP = arraySale[178],
                            //**********************************************

                            C_F_FA__Tarifa = arraySale[179],
                            C_F_FM__Tarifa_MP50 = arraySale[180],
                            C_F_FE__Tarifa_MP10 = arraySale[181],
                            C_E_EQ__Eqpd = arraySale[182],
                            C_E_EM__Eqpd_MP50 = arraySale[183],
                            C_E_EE__Eqpd_MP10 = arraySale[184],
                            C_E_EP__Eqpd_MP61 = arraySale[185],
                            M__Miscelaneo = arraySale[186],
                            Desc_Motivo = arraySale[187],
                            Sub_Tipo = arraySale[188],
                            Desc_Sub_Tipo = arraySale[189],
                            Inf_Adicional = arraySale[190],
                            Metodo = arraySale[191],
                            Nro_Cupon = arraySale[192],
                            STPO = arraySale[193],
                            Origen = arraySale[194],
                            Destino = arraySale[195],
                            Carrier = arraySale[196],
                            Vuelo = arraySale[197],
                            Clase = arraySale[198],
                            Fecha_Programada_de_vuelo = arraySale[199],
                            Moneda_Revenue = arraySale[200],
                            Valor_Revenue = arraySale[201],
                            Nro_Cupon60 = arraySale[202],
                            STPO61 = arraySale[203],
                            Origen62 = arraySale[204],
                            Destino63 = arraySale[205],
                            Carrier64 = arraySale[206],
                            Vuelo65 = arraySale[207],
                            Clase66 = arraySale[208],
                            Fecha_Programada_de_vuelo67 = arraySale[209],
                            Moneda_Revenue68 = arraySale[210],
                            Valor_Revenue69 = arraySale[211],
                            Nro_Cupon70 = arraySale[212],
                            STPO71 = arraySale[213],
                            Origen72 = arraySale[214],
                            Destino73 = arraySale[215],
                            Carrier74 = arraySale[216],
                            Vuelo75 = arraySale[217],
                            Clase76 = arraySale[218],
                            Fecha_Programada_de_vuelo77 = arraySale[219],
                            Moneda_Revenue78 = arraySale[220],
                            Valor_Revenue79 = arraySale[221],
                            Nro_Cupon80 = arraySale[222],
                            STPO81 = arraySale[223],
                            Origen82 = arraySale[224],
                            Destino83 = arraySale[225],
                            Carrier84 = arraySale[226],
                            Vuelo85 = arraySale[227],
                            Clase86 = arraySale[228],
                            Fecha_Programada_de_vuelo87 = arraySale[229],
                            Moneda_Revenue88 = arraySale[230],
                            Valor_Revenue89 = arraySale[231],
                            ConjTKT = arraySale[232],
                            Fare_Calc_Mode = arraySale[233],
                            Itinerario = arraySale[234],
                            C_T_YR__YR = arraySale[235],
                            C_T_CP__CP = arraySale[236],
                            C_T_QV__TASA_DE_SEGURIDAD = arraySale[237],
                            C_T_OG__Aviation_Safety_and_Secur = arraySale[238],
                            C_T_JD__Departure_Charge__Interna = arraySale[239],
                            C_T_HW__Airport_Departure_Tax__In = arraySale[240],
                            C_T_DY__Tourism_Arrival_Tax = arraySale[241],
                            C_T_YQ__YQ = arraySale[242],
                            C_T_AA__Airport_Departure_Tax__In = arraySale[243],
                            C_T_VB__Airport_Infrastructure_Fe = arraySale[244],
                            C_T_UX__Airport_Authority_Fee = arraySale[245],
                            C_T_SF__Service_Fee = arraySale[246],
                            C_T_PE__Sales_Tax__International_ = arraySale[247],
                            C_T_DO__Transportation_Tax__Inter = arraySale[248],
                            C_T_OD__PENALIZACION_POR_CAMBIO = arraySale[249],
                            C_T_OB__PENALIZACION_POR_CAMBIO = arraySale[250],
                            C_T_MF__MISCELLANEOUS_FEE = arraySale[251],
                            C_T_ZQ__Airport_Facility_Charge_ = arraySale[252],
                            C_T_CU__CUBA_INTERN = arraySale[253],
                            C_T_ES__Sales_Tax__Nacional_ = arraySale[254],
                            C_T_AK__Airport_Departure_Tax_CCS = arraySale[255],
                            C_T_AJ__AJ = arraySale[256],
                            C_T_YN__YN = arraySale[257],
                            C_T_EU__EU = arraySale[258],
                            C_T_6A__FEE_EMISION_PERU = arraySale[259],
                            C_T_C2__BIOSECURITY_SERVICE_FEE = arraySale[260],
                            C_T_E2__Tax_Arrivals_Ecuador = arraySale[261],
                            C_T_QI__Airport_Auxiliary_Facilit = arraySale[262],
                            C_T_QB__Airport_Tax_ = arraySale[263],
                            C_T_WT__Security_Fee_ = arraySale[264],
                            C_T_OR__DOMESTIC_AIRPORT_TAX_ = arraySale[265],
                            C_T_EC__Goverment_trasport_Inter = arraySale[266],
                            C_T_ED__Tourism_Fee_Intern_salida = arraySale[267],
                            C_T_PA__IVA_PANAMA = arraySale[268],
                            C_T_DG__Resident_Exit_Tax = arraySale[269],
                            C_T_CO__Domestic_and_Intl = arraySale[270],
                            C_T_YS__Sales_Tax_VAT = arraySale[271],
                            C_T_JS__Tourism_Tax = arraySale[272],
                            C_T_OC__Service_Fee_Memos = arraySale[273],
                            C_T_GQ__IMPUESTO_DE_SALIDA = arraySale[274],
                            C_T_M9__CARGO_DE_SEGURIDAD_OPERAC = arraySale[275],
                            C_T_N2__CARGO_DE_SEGURIDAD_DE_AVI = arraySale[276],
                            C_T_LL__IMPUESTO_DE_SEGURIDAD = arraySale[277],
                            C_T_N1__CUOTA_DE_DESARROLLO_DE_IN = arraySale[278]

                        }
                        #endregion

                    ); 


                    }
                    catch (Exception ex)
                    {

                        ViewBag.Error = "Algunos registro no pudieron se cargados";
                        return View(db.VentasKiuAdminFile.OrderByDescending(o => o.UserFec));

                    }

                }
            }

            TMPventasKiu.RemoveAt(0); // Para quitar los titulos

            
            
            using (var context = new IntranetDBEntities())
            {
                List<TMPVentasKiuAdmin> _ventasKiu = TMPventasKiu;
                context.BulkInsert<TMPVentasKiuAdmin>(_ventasKiu);
                ViewBag.Error = "Archivo cargado correctamente";
                ViewBag.Mensaje = "Archivo cargado correctamente";
            }


            VentasKiuAdminFile _ventasKiuAdminFile1 = db.VentasKiuAdminFile.First(w => w.Nombre == filename);
            _ventasKiuAdminFile1.Estado = 1;  //Cargando
            db.SaveChanges();


            var procesado = db.ValidarDuplicadosVentas(filename);

            VentasKiuAdminFile _ventasKiuAdminFile2 = db.VentasKiuAdminFile.First(w => w.Nombre == filename);

            var resultado = procesado.FirstOrDefault();
            _ventasKiuAdminFile2.Estado = 2; //Procesado
            _ventasKiuAdminFile2.Procesado = resultado.Procesado;
            _ventasKiuAdminFile2.Nuevo = resultado.Nuevo;
            _ventasKiuAdminFile2.Actualizado = resultado.Actualizado;
            db.SaveChanges();




            //db.SaveChanges();
            //int i = 0;
            //foreach (var item in ventasKiu)
            //{
            //    i++;
            //    try
            //    {
            //        db.VentasKiuAdmin.Add(item);


            //        if (i % batchSave == 0) 
            //        { 
            //            db.SaveChanges(); 
            //        }
            //    }
            //    catch (DbEntityValidationException e)
            //    {
            //        foreach (var eve in e.EntityValidationErrors)
            //        {
            //            Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
            //                eve.Entry.Entity.GetType().Name, eve.Entry.State);
            //            foreach (var ve in eve.ValidationErrors)
            //            {
            //                Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
            //                    ve.PropertyName, ve.ErrorMessage);
            //            }
            //        }

            //    }

            //}

            
            
            return View(db.VentasKiuAdminFile.OrderByDescending(o => o.UserFec).ToList());
        }



        [HttpGet]
        public ActionResult Resumen()
        {

            Utils utils = new Utils();
            ViewBag.Month = utils.ListarMeses(DateTime.Now.Month);
            ViewBag.Year = utils.ListaAño();

            ResumenCanal resumenCanal = new ResumenCanal(DateTime.Now.Year,DateTime.Now.Month);
            

            return View(resumenCanal);

            
        }
        [HttpPost]
        public ActionResult Resumen(int Year, int Month)
        {

            Utils utils = new Utils();

            ViewBag.Month = utils.ListarMeses(Month);
            ViewBag.Year = utils.ListaAño();


            ResumenCanal resumenCanal = new ResumenCanal(Year, Month);


            return View(resumenCanal);


        }

        [HttpGet]
        public ActionResult ResumenDiario()
        {

            Utils utils = new Utils();

            ViewBag.Month = utils.ListarMeses(DateTime.Now.Month);
            ViewBag.Year = utils.ListaAño();

            ResumenCanalDiario resumenCanalDiario = new ResumenCanalDiario(DateTime.Now.Year, DateTime.Now.Month);
            

            return View(resumenCanalDiario);

        }

        [HttpPost]
        public ActionResult ResumenDiario(int Year, int Month)
        {

            Utils utils = new Utils();

            ViewBag.Month = utils.ListarMeses(Month);
            ViewBag.Year = utils.ListaAño();


            ResumenCanalDiario resumenCanalDiario = new ResumenCanalDiario(Year, Month);


            return View(resumenCanalDiario);


        }
        [HttpGet]
        public ActionResult ResumenDocumento()
        {

            Utils utils = new Utils();

            ViewBag.Month = utils.ListarMeses(DateTime.Now.Month);
            ViewBag.Year = utils.ListaAño();


            ResumenCanalDocumento resumenCanalDocumento = new ResumenCanalDocumento(DateTime.Now.Year, DateTime.Now.Month);


            return View(resumenCanalDocumento);


        
        }
        [HttpPost]
        public ActionResult ResumenDocumento(int Year, int Month)
        {

            Utils utils = new Utils();

            ViewBag.Month = utils.ListarMeses(Month);
            ViewBag.Year = utils.ListaAño();


            ResumenCanalDocumento resumenCanalDocumento = new ResumenCanalDocumento(Year, Month);


            return View(resumenCanalDocumento);


        }

        [HttpGet]
        public PartialViewResult VentaCanalDetalle(int month, int anio, string canal)
        {
            List<VentaCanalDetalle_Result> result = db.VentaCanalDetalle(anio, month, canal).ToList();
            return PartialView("_VentaCanalDetalle", result);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
               db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
