using System;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;

namespace CleanBaseline_eric
{
    public static class SelectAndExportActions
    {
        public static void CopiarParaNovoArquivo(Document doc)
        {
            Editor ed = doc.Editor;
            Database db = doc.Database;

            ObjectIdCollection selectedObjIds = new ObjectIdCollection();

            // 1. Travar o documento original para fazer a seleção com segurança
            using (DocumentLock docLock = doc.LockDocument())
            {
                PromptSelectionOptions pso = new PromptSelectionOptions();
                pso.MessageForAdding = "\nSelect the elements to copy (Press ESC to exit): ";
                PromptSelectionResult psr = ed.GetSelection(pso);

                if (psr.Status != PromptStatus.OK)
                {
                    ed.WriteMessage("\nCommand canceled. No elements selected.");
                    return;
                }

                selectedObjIds = new ObjectIdCollection(psr.Value.GetObjectIds());

                PromptKeywordOptions pko = new PromptKeywordOptions(
                    $"\nSelected {selectedObjIds.Count} elements. Create a new drawing? [Yes/No] ",
                    "Yes No"
                );
                pko.Keywords.Default = "Yes";
                PromptResult pr = ed.GetKeywords(pko);

                if (pr.Status != PromptStatus.OK || pr.StringResult == "No")
                {
                    ed.WriteMessage("\nOperation canceled.");
                    return;
                }
            } // O documento original é destravado aqui

            // 2. Criar o novo arquivo e copiar os elementos mantendo a posição de origem
            try
            {
                // Cria um novo arquivo em branco
                Document newDoc = Application.DocumentManager.Add("acad.dwt");
                Database newDb = newDoc.Database;

                // Trava o novo arquivo para escrever nele
                using (DocumentLock newDocLock = newDoc.LockDocument())
                {
                    using (Transaction tr = newDb.TransactionManager.StartTransaction())
                    {
                        BlockTable bt = (BlockTable)tr.GetObject(newDb.BlockTableId, OpenMode.ForRead);
                        BlockTableRecord newModelSpace = (BlockTableRecord)tr.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite);

                        // WblockCloneObjects copia mantendo a posição original
                        IdMapping mapping = new IdMapping();
                        db.WblockCloneObjects(selectedObjIds, newModelSpace.ObjectId, mapping, DuplicateRecordCloning.Ignore, false);

                        tr.Commit();
                    }
                }

                Application.DocumentManager.MdiActiveDocument = newDoc;
                newDoc.Editor.WriteMessage($"\nSuccess! {selectedObjIds.Count} elements were copied to their original coordinates.");
            }
            catch (System.Exception ex)
            {
                doc.Editor.WriteMessage($"\nAn error occurred: {ex.Message}");
            }
        }
    }
}