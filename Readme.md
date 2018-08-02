# How to Generate persistent metadata for an arbitrary data table

For this task, XPO provides the capability to define persistent classes on the fly, without defining any classes in code, except for the class that will be used as the base class for dynamically created metadata. This class should provide a constructor with the XPClassInfo parameter. The code below is quite enough for this:

```csharp
[NonPersistent]
public class BasePersistentClass :XPLiteObject {
   public BasePersistentClass(Session session) : base(session) { }
   public BasePersistentClass(Session session, XPClassInfo classInfo) : base(session, classInfo) { }
}
```

```vbnet
<NonPersistent> _
Public Class BasePersistentClass
    Inherits XPLiteObject
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

    Public Sub New(ByVal session As Session, ByVal classInfo As XPClassInfo)
        MyBase.New(session, classInfo)
    End Sub
End Class
```

You can create virtual classes using the [XPDictionary.CreateClass](https://documentation.devexpress.com/CoreLibraries/DevExpress.Xpo.Metadata.XPDictionary.CreateClass.overloads) method. This method creates a new instance of [XPClassInfo](https://documentation.devexpress.com/CoreLibraries/DevExpress.Xpo.Metadata.XPClassInfo.class), and also adds it to the [XPDictionary.Classes](https://documentation.devexpress.com/CoreLibraries/DevExpress.Xpo.Metadata.XPDictionary.Classes.property) collection. The [XPClassInfo.CreateMember](https://documentation.devexpress.com/CoreLibraries/DevExpress.Xpo.Metadata.XPClassInfo.CreateMember.overloads) method will create a new [XPCustomMemberInfo](https://documentation.devexpress.com/CoreLibraries/DevExpress.Xpo.Metadata.XPCustomMemberInfo.class) instance, and add it to the [XPClassInfo.Members](https://documentation.devexpress.com/CoreLibraries/DevExpress.Xpo.Metadata.XPClassInfo.Members.property) collection. For example:

```charp
XPDictionary dictionary = new ReflectionDictionary();
XPClassInfo classInfo = dictionary.CreateClass(dictionary.GetClassInfo(typeof(BasePersistentClass)), "MyClass");
XPMemberInfo memberInfo = classInfo.CreateMember("MyProperty", typeof(string));
```

```vbnet
Dim dictionary As XPDictionary = New ReflectionDictionary()
Dim classInfo As XPClassInfo = dictionary.CreateClass(dictionary.GetClassInfo(GetType(BasePersistentClass)), "MyClass")
Dim memberInfo As XPMemberInfo = classInfo.CreateMember("MyProperty", GetType(String))
```

The ConnectionProviderSql class provides two methods that can be used to retrieve the database schema. The ConnectionProviderSql.GetStorageTablesList method returns an array of string values, representing table names in the database. The table names can be passed as parameters to the ConnectionProviderSql.GetStorageTables method, which will return an array of DBTable instances, describing the schema of specified tables. Here is the code of the method that creates persistent metadata based on a DBTable instance:

```chaarp
public static XPClassInfo AddClass(XPDictionary dict, DBTable table) {
   if (table.PrimaryKey.Columns.Count > 1) 
       throw new NotSupportedException("Compound primary keys are not supported");
   XPClassInfo classInfo = dict.CreateClass(dict.GetClassInfo(typeof(BasePersistentClass)), table.Name.Replace('.', '_'));
   classInfo.AddAttribute(new PersistentAttribute(table.Name));
   DBColumnType primaryColumnType = table.GetColumn(table.PrimaryKey.Columns[0]).ColumnType;
   classInfo.CreateMember(table.PrimaryKey.Columns[0], DBColumn.GetType(primaryColumnType), 
       new KeyAttribute(IsAutoGenerationSupported(primaryColumnType)));
   foreach (DBColumn col in table.Columns)
       if (!col.IsKey)
           classInfo.CreateMember(col.Name, DBColumn.GetType(col.ColumnType));
   return classInfo;
}

static bool IsAutoGenerationSupported(DBColumnType type) {
   return type == DBColumnType.Guid || type == DBColumnType.Int16 || type == DBColumnType.Int32;
}
```

```vbnet
Public Shared Function AddClass(ByVal dict As XPDictionary, ByVal table As DBTable) As XPClassInfo
    If table.PrimaryKey.Columns.Count > 1 Then
        Throw New NotSupportedException("Compound primary keys are not supported")
    End If
    Dim classInfo As XPClassInfo = dict.CreateClass(dict.GetClassInfo(GetType(BasePersistentClass)), table.Name.Replace("."c, "_"c))
    classInfo.AddAttribute(New PersistentAttribute(table.Name))
    Dim primaryColumnType As DBColumnType = table.GetColumn(table.PrimaryKey.Columns(0)).ColumnType
    classInfo.CreateMember(table.PrimaryKey.Columns(0), DBColumn.GetType(primaryColumnType), New KeyAttribute(IsAutoGenerationSupported(primaryColumnType)))
    For Each col As DBColumn In table.Columns
        If (Not col.IsKey) Then
            classInfo.CreateMember(col.Name, DBColumn.GetType(col.ColumnType))
        End If
    Next col
    Return classInfo
End Function

Shared Function IsAutoGenerationSupported(ByVal type As DBColumnType) As Boolean
    Return type = DBColumnType.Guid OrElse type = DBColumnType.Int16 OrElse type = DBColumnType.Int32
End Function
```

The implementation of the method is quite simple. The method simply iterates through the DBTable.Columns collection, and adds a persistent property to metadata based on each DBColumn instance. The key property is generated separately, to check some necessary conditions. For example, it is impossible to create runtime metadata mapped to a table with a compound key.

XPDictionary can be passed as a parameter to the constructors of the [SimpleDataLayer](https://documentation.devexpress.com/CoreLibraries/DevExpress.Xpo.SimpleDataLayer.class) and [ThreadSafeDataLayer](https://documentation.devexpress.com/CoreLibraries/DevExpress.Xpo.ThreadSafeDataLayer.class) classes.

```csharp
XPClassInfo classInfo = session1.GetClassInfo(string.Empty, "Categories");
gridControl1.DataSource = new XPCollection(session1, classInfo);
```

```vbnet
Dim classInfo As XPClassInfo = session1.GetClassInfo(String.Empty, "Categories")
gridControl1.DataSource = New XPCollection(session1, classInfo)
```
**See Also:**
[How to create an XPClassInfo descendant to dynamically build a persistent class structure](https://github.com/DevExpress-Examples/how-to-create-an-xpclassinfo-descendant-to-dynamically-build-a-persistent-class-structure-e1729)
[How to generate persistent classes at runtime based on a dataset](https://github.com/DevExpress-Examples/how-to-generate-persistent-classes-at-runtime-based-on-a-dataset-e1198)
[How to create collection properties with associations at runtime](https://github.com/DevExpress-Examples/how-to-create-collection-properties-with-associations-at-runtime-e5139)
[How to dynamically create a read-only calculated (persistent alias) property](https://github.com/DevExpress-Examples/how-to-dynamically-create-a-read-only-calculated-persistent-alias-property-e3473)
[How to create persistent classes mapped to tables with a composite primary key at runtime](https://github.com/DevExpress-Examples/how-to-create-persistent-classes-mapped-to-tables-with-a-composite-primary-key-at-runtime-e4606)
[eXpressApp Framework](https://docs.devexpress.com/eXpressAppFramework/112670/index) > [Concepts](https://docs.devexpress.com/eXpressAppFramework/112683/concepts) > [Business Model Design](https://docs.devexpress.com/eXpressAppFramework/113461/concepts/business-model-design) > [Types Info Subsystem](https://docs.devexpress.com/eXpressAppFramework/113669/concepts/business-model-design/types-info-subsystem) > [Customize Business Object's Metadata](https://docs.devexpress.com/eXpressAppFramework/113583/concepts/business-model-design/types-info-subsystem/customize-business-object's-metadata)
