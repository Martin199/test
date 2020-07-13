using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LetraModel;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            

            List<string> listaSinCodificar = new List<string>();



            string fraseSinCodificar = Convert.ToString(Console.ReadLine());
            string palabraConEspacio = string.Empty;

            for (int i = 1; i <= fraseSinCodificar.Length; i++)
            {
                if ((i % 5) == 0 )
                {
                    palabraConEspacio += fraseSinCodificar[i - 1];
                    listaSinCodificar.Add(palabraConEspacio);
                    palabraConEspacio = string.Empty;
                }
                else
                {
                    palabraConEspacio += fraseSinCodificar[i - 1];
                }
            }
            if (palabraConEspacio != string.Empty)
            {
                listaSinCodificar.Add(palabraConEspacio);
            }

            string fileJson = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "codigo.json"));

            List<LetraModel> listLetraModel = JsonConvert.DeserializeObject<List<LetraModel>>(fileJson);
            
            
            var data = codificarLista(listLetraModel, listaSinCodificar);

            Console.WriteLine(data);

            Console.WriteLine(fileJson.Replace("{","").Replace("}", "").Replace("[","").Replace("]","").Replace(",",""));

            Console.ReadKey();
            

        }

        static public string codificarLista(List<LetraModel> listLetraModel, List<string> listaSinCodificar)
        {

            string fraseCodificada = "";
            int ii = 0;

            foreach (var item in listaSinCodificar)
            {
                for (int i = 0; i < item.Length; i++)
                {
                    string letraPalabra = item[i].ToString();
                    LetraModel letraExistente = listLetraModel.Where(x => x.Letra == letraPalabra).FirstOrDefault();
                    if (letraExistente != null)
                    {

                        if (fraseCodificada.Contains(letraExistente.Codigo.ToString()))
                        {
                            letraExistente.Codigo++;
                        }
                         fraseCodificada += " " + letraExistente.Codigo.ToString() + "_" + ii.ToString();
                    }

                    

                }
                ii++;
                fraseCodificada += "\n";
            }



            return fraseCodificada;
        }
    }


    
}