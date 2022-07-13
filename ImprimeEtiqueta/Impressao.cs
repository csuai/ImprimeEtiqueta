using System.Diagnostics;
using System.Text.RegularExpressions;

using static System.Net.Mime.MediaTypeNames;

namespace ImprimeEtiqueta;

public static class Impressao
{
    public static string Etiqueta(string etiqueta, IConfiguration configuration, int etiquetasLinha)
    {
        int tipoRolo = configuration.GetValue<int>("TipoRolo");
        if (tipoRolo != etiquetasLinha)
        {
            throw new Exception($"Impressora contendo rolo com {tipoRolo} etiqueta(s) por linha");
        }
        etiqueta=AcertaDensidadeImpressao(etiqueta, configuration["DensidadeImpressao"]);
        string impressora = configuration["EnderecoImpressora"];
        string localizacaoEtiqueta = configuration["LocalizacaoEtiqueta"];
        File.WriteAllText(localizacaoEtiqueta, etiqueta);

        ProcessStartInfo processInfo = new ProcessStartInfo("cmd.exe", "/c " + @"Type " + localizacaoEtiqueta + @" > " + impressora);

        processInfo.CreateNoWindow = true;
        processInfo.UseShellExecute = false;

        var process = Process.Start(processInfo);
        process?.WaitForExit();

        process?.Close();

        return etiqueta;
}
    static string AcertaDensidadeImpressao(string etiqueta, string densidade)
    {
        return etiqueta.Replace("D13", densidade);
    }
}
