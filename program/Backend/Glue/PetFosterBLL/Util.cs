using System.Data;

namespace Glue.PetFoster.BLL
{
    public class Util
    {
        public static void DebugTable(DataTable dt)
        {
            foreach (DataColumn column in dt.Columns)
            {
                Console.Write("{0,-20}", column.ColumnName);
            }
            Console.WriteLine();

            foreach (DataRow row in dt.Rows)
            {
                foreach (var item in row.ItemArray)
                {
                    Console.Write("{0,-20}", item);
                }
                Console.WriteLine();
            }
        }
    }
}
