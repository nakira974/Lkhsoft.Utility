using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Schema;
using Microsoft.CodeAnalysis;
//using Microsoft.CodeAnalysis.CSharp;
//using Microsoft.CodeAnalysis.Emit;
namespace Lkhsoft.Utility.Serialization.Implementations;

public class XmlToCsharpSerializer
{
    #region
    /**
    public static object? Deserialize(string xmlFilePath, string className)
    {
        var schemaSet = new XmlSchemaSet();
        var reader    = XmlReader.Create(xmlFilePath);
        while (reader.Read())
        {
            if (reader.NodeType != XmlNodeType.Element || reader.LocalName != "xs:schema") continue;
            var schema = XmlSchema.Read(reader, null);
            if (schema != null) schemaSet.Add(schema);
        }

        var provider = CodeDomProvider.CreateProvider("CSharp");
        var options = new CompilerParameters()
                      {
                          GenerateInMemory      = true,
                          TreatWarningsAsErrors = false
                      };
        
        var compileUnit = new CodeCompileUnit();
        var importer = new XmlSchemas();
        importer.Add(schemaSet);
        var nsmgr = new XmlNamespaceManager(new NameTable());
        nsmgr.AddNamespace("xs", "http://www.w3.org/2001/XMLSchema");
        Type? type;
        foreach (XmlSchema schema in schemaSet.Schemas())
        {
            var codeNamespace = new CodeNamespace();
            codeNamespace.Name = "GeneratedNamespace";
            foreach (XmlSchemaObject item in schema.Items)
            {
                type = item as XmlSchemaType ?? throw new InvalidOperationException("Unrecognized xml type");
                AddType(type, codeNamespace.Types, importer, nsmgr);
                var element = item as XmlSchemaElement;
                if (element == null) continue;
                var typeReference = GetTypeReference(element.SchemaTypeName, importer, nsmgr);
                var field         = new CodeMemberField(typeReference, element.Name ?? throw new InvalidOperationException("No xml element name!"))
                                    {
                                        Attributes = MemberAttributes.Public
                                    };
                
                codeNamespace.Types.Add(field);
            }
            compileUnit.Namespaces.Add(codeNamespace);
        }

        var sourceCodeWriter = new StringWriter();

        CodeGeneratorOptions codeOptions = new CodeGeneratorOptions() { BlankLinesBetweenMembers = false };
        codeOptions.BracingStyle = "C";
        CodeDomProvider languageProvder = CodeDomProvider.CreateProvider("c#");

        languageProvder.GenerateCodeFromCompileUnit(compileUnit, sourceCodeWriter, codeOptions);

        var results =
            CodeDomProvider.CreateProvider("c#").CompileAssemblyFromSource(
                                                                           options,
                                                                           new[] {sourceCodeWriter.ToString()});
 		
        if (results.Errors.HasErrors)
            throw new InvalidOperationException($"Failed to compile generated code: {string.Join(", ", results.Errors.Cast<CompilerError>().Select(x => x.ErrorText))}");
		
        var assembly = AppDomain.CurrentDomain.Load(results.CompiledAssembly.GetName());	 
        
        type = assembly.GetType(className);
        return Activator.CreateInstance(type ?? throw new InvalidOperationException("Could not create Activator's instance!"));
    }
    
    private static readonly Dictionary<XmlSchemaType?, CodeTypeDeclaration> TypeMappings = new Dictionary<XmlSchemaType?, CodeTypeDeclaration>();
    private static void AddType(XmlSchemaType? type, CodeTypeDeclarationCollection types, XmlSchemas importer, XmlNamespaceManager nsmgr)
    {
        if (TypeMappings.ContainsKey(type ?? throw new ArgumentNullException(nameof(type))))
        {
            return;
        }

        var typeName = GetUniqueTypeName(type?.Name ?? string.Empty, types);
        var codeType = new CodeTypeDeclaration(typeName);
        types.Add(codeType);
        TypeMappings[type ?? throw new ArgumentNullException(nameof(type))] = codeType;

        if (type is not XmlSchemaComplexType complexType) return;
        var contentModel = complexType.ContentModel;
        var particle     = contentModel?.Content;
        var sequence     = (XmlSchemaSequence) particle;
        if (sequence == null) return;
        foreach (var xmlSchemaObject in sequence.Items)
        {
            var childElement = (XmlSchemaElement) xmlSchemaObject;
            var property = new CodeMemberProperty
                           {
                               Attributes = MemberAttributes.Public,
                               Type       = GetTypeReference(childElement.SchemaTypeName, importer, nsmgr),
                               Name       = childElement.Name ?? throw new InvalidOperationException("No child element name!")
                           };
            codeType.Members.Add(property);
        }
    }

    private static string GetUniqueTypeName(string baseName, CodeTypeDeclarationCollection types)
    {
        var uniqueName = baseName;
        var appendCount = 1;
        while (types.Cast<CodeTypeDeclaration>().Any(x => x.Name == uniqueName))
        {
            uniqueName = $"{baseName}{appendCount++}";
        }
        return uniqueName;
    }

    private static CodeTypeReference GetTypeReference(XmlQualifiedName xmlQualifiedName, XmlSchemas importer, XmlNamespaceManager nsmgr)
    {
        while (true)
        {
            var schema = importer[xmlQualifiedName.Namespace];
            if (schema == null) return new CodeTypeReference(typeof(object));
            var type = schema.SchemaTypes[new XmlQualifiedName(xmlQualifiedName.Name)];
            if (type == null) return new CodeTypeReference(typeof(object));
            var codeType = TypeMappings.TryGetValue(type as XmlSchemaType, out var typeMapping) ? typeMapping : null;
            if (codeType != null)
            {
                return new CodeTypeReference(codeType.Name);
            }

            var simpleType = type as XmlSchemaSimpleType;
            if (simpleType?.Content is XmlSchemaSimpleTypeRestriction restriction)
            {
                if (restriction.BaseTypeName == xmlQualifiedName) // Just a simple type alias
                {
                    return new CodeTypeReference(restriction.BaseTypeName.Name);
                }

                var codeTypeReference = GetTypeReference(restriction.BaseTypeName, importer, nsmgr);
                if (restriction.Facets.Cast<XmlSchemaFacet>().Any(x => x is XmlSchemaEnumerationFacet)) // This is an enum
                {
                    codeTypeReference = new CodeTypeReference(typeof(Enum));
                }

                var nullable = !restriction.BaseTypeName.IsEmpty && restriction.BaseTypeName != XmlQualifiedName.Empty;
                return new CodeTypeReference(nullable ? typeof(Nullable<>) : typeof(ValueType),
                                             codeTypeReference);
            }

            if (type is not XmlSchemaComplexType complexType) return new CodeTypeReference(typeof(object));
            var particle = complexType.ContentTypeParticle;
            if (particle is not XmlSchemaSequence sequence || sequence.Items.Count != 1 || particle.MinOccurs != 1 || particle.MaxOccurs != 1) return new CodeTypeReference(typeof(object));
            if (sequence.Items[0] is not XmlSchemaElement element) return new CodeTypeReference(typeof(object));
            xmlQualifiedName = element.SchemaTypeName;
            continue;

            // Unsupported type
            break;
        }
    }**/
    #endregion Converter
}