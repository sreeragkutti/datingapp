public DataTable ConvertToDataTable(List<Model> items)
        {
            DataTable dataTable = new DataTable(typeof(Model).Name);
            PropertyInfo[] Props = typeof(Model).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            dataTable.Columns.Add("Sl. No");
            d
            var slno = 1;

            foreach (Model item in items)
            {
                
                var values = new object[Props.Length+1];
                values[0] = slno;
                values[1] = Props[0].GetValue(item, null);
                values[2] = Props[1].GetValue(item, null);
                .............

                dataTable.Rows.Add(values);
                slno++;

            }
            return dataTable;

        }