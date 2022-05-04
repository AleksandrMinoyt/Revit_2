using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;

namespace Revit_2
{
    [TransactionAttribute(TransactionMode.Manual)]
    public class CopyGroup : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uiDoc = commandData.Application.ActiveUIDocument;
            Document doc = uiDoc.Document;
            
            Reference reference = uiDoc.Selection.PickObject(ObjectType.Element, "Выберите группу объектов");
            Element element= doc.GetElement(reference);
            Group group = element as Group;

            if (group!=null)
            {

                XYZ point = uiDoc.Selection.PickPoint("Выберите точку");

                Transaction trans = new Transaction(doc);
                trans.Start("Копируем группу");
                doc.Create.PlaceGroup(point, group.GroupType);
                trans.Commit();
            }
            else
            {
                message = "Не выбрана группа";
                return Result.Failed;
            }

            message = "Группа скопирована";
            return Result.Succeeded;


        }
    }
}
