using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;

namespace CleanBaseline_eric
{
    public class Commands
    {
        // A flag Session é necessária aqui no ponto de entrada
        [CommandMethod("CleanBaseEric", CommandFlags.Session)]
        public void CleanBaseEricCommand()
        {
            // Pega o documento atual e verifica se é válido
            Document doc = Application.DocumentManager.MdiActiveDocument;
            if (doc == null) return;

            // Chama o método separado que fará todo o trabalho pesado
            SelectAndExportActions.CopiarParaNovoArquivo(doc);
        }
    }
}