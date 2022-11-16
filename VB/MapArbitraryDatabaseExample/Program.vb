Imports System
Imports DevExpress.Xpo
Imports DevExpress.Xpo.DB
Imports DevExpress.Xpo.Metadata
Imports DXSample
Imports System.Data.SQLite

Namespace MapArbitraryDatabaseExample

    Friend Module Program

        ''' <summary>
        ''' The main entry point for the application.
        ''' </summary>
        <STAThread>
        Sub Main()
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(False)
            Dim connectionString As String = SQLiteConnectionProvider.GetConnectionString("nwind.sqlite")
            Using connection As IDbConnection = New SQLiteConnection(connectionString)
                Program.InitializeDataLayer(connection)
                Application.Run(New Form1())
            End Using
        End Sub

        Private Sub InitializeDataLayer(ByVal connection As IDbConnection)
            Dim prov As ConnectionProviderSql = CType(XpoDefault.GetConnectionProvider(connection, AutoCreateOption.None), ConnectionProviderSql)
            Dim dict As XPDictionary = New ReflectionDictionary()
            Dim tables As DBTable() = prov.GetStorageTables("Category")
            AddClass(dict, tables(0))
            XpoDefault.DataLayer = New SimpleDataLayer(dict, prov)
        End Sub

        Public Function AddClass(ByVal dict As XPDictionary, ByVal table As DBTable) As XPClassInfo
            If table.PrimaryKey.Columns.Count > 1 Then Throw New NotSupportedException("Compound primary keys are not supported")
            Dim classInfo As XPClassInfo = dict.CreateClass(dict.GetClassInfo(GetType(BasePersistentClass)), table.Name.Replace("."c, "_"c))
            classInfo.AddAttribute(New PersistentAttribute(table.Name))
            Dim primaryColumnType As DBColumnType = table.GetColumn(table.PrimaryKey.Columns(0)).ColumnType
            classInfo.CreateMember(table.PrimaryKey.Columns(0), DBColumn.[GetType](primaryColumnType), New KeyAttribute(IsAutoGenerationSupported(primaryColumnType)))
            For Each col As DBColumn In table.Columns
                If Not col.IsKey AndAlso col.ColumnType IsNot DBColumnType.Unknown Then classInfo.CreateMember(col.Name, DBColumn.[GetType](col.ColumnType))
            Next

            Return classInfo
        End Function

        Private Function IsAutoGenerationSupported(ByVal type As DBColumnType) As Boolean
            Return type Is DBColumnType.Guid OrElse type Is DBColumnType.Int16 OrElse type Is DBColumnType.Int32
        End Function
    End Module
End Namespace
