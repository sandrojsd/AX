using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Xamarin.Forms;

[assembly: Dependency(typeof(Files.WinPhone.File))]

namespace Files.WinPhone {

    public class File : IFile {

        public string Caminho
        {
	        get { return Windows.Storage.ApplicationData.Current.LocalFolder.Path; }
        }

        public string Ler(string filename)
        {
            var task = CarregaDadosArquivo(filename);
            task.Wait();
            return task.Result;
        }
        async Task<string> CarregaDadosArquivo(string filename)
        {
            StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;
            if(local!=null)
            {
                var file = await local.GetItemAsync(filename);
                using (StreamReader streamReader = new StreamReader(file.Path))
                    return streamReader.ReadToEnd();
            }
            return "";
        }

        public async Task Salvar(string filename, string text)
        {
            StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;

            var file = await local.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);

            using (StreamWriter writer = new StreamWriter(await file.OpenStreamForWriteAsync()))
                writer.Write(text);
        }
       
    }
}
