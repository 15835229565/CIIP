using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using CIIP.Module.BusinessObjects.SYS;
using Mono.Cecil;

namespace CIIP
{
    public static class BuilderHelper
    {
        public static Dictionary<BusinessObjectBase, TypeReference> TypeReferences { get; } = new Dictionary<BusinessObjectBase, TypeReference>();


        public static void InitializeTypeReferences(this ModuleDefinition module,
            IEnumerable<BusinessObjectBase> dllTypes)
        {
            #region ����TypeReferences�����潫ʹ��

            foreach (var dlltype in dllTypes)
            {
                var t = ReflectionHelper.FindType(dlltype.FullName);
                if (t == null)
                    throw new Exception("û���ҵ�����:" + dlltype.FullName);
                var tr = module.ImportReference(t);
                if (tr == null)
                    throw new Exception("û���ҵ�cecil����!" + dlltype.FullName);
                TypeReferences.Add(dlltype, tr);
            }

            #endregion
        }

        public static void AddToTypeReferences(this TypeReference self, BusinessObjectBase bo)
        {
            TypeReferences.Add(bo, self);
        }

        public static TypeDefinition GetTypeDefintion(this BusinessObject bo)
        {
            return TypeReferences[bo] as TypeDefinition;
        }

        public static TypeReference GetTypeReference(this BusinessObjectBase bo)
        {
            try
            {
                if (TypeReferences.ContainsKey(bo))
                {
                    return TypeReferences[bo];
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static TypeReference MakeGenericType(this TypeReference self, params TypeReference[] arguments)
        {
            if (self.GenericParameters.Count != arguments.Length)
                throw new ArgumentException();

            var instance = new GenericInstanceType(self);
            foreach (var argument in arguments)
                instance.GenericArguments.Add(argument);

            return instance;
        }

        public static MethodReference MakeGenericMethod(this MethodReference self, params TypeReference[] arguments)
        {
            //if (self.GenericParameters.Count != arguments.Length)
            //    throw new ArgumentException();

            var instance = new GenericInstanceMethod(self);
            foreach (var argument in arguments)
                instance.GenericArguments.Add(argument);

            return instance;
        }

        public static MethodReference MakeGeneric(this MethodReference self, params TypeReference[] arguments)
        {
            var reference = new MethodReference(self.Name, self.ReturnType)
            {
                DeclaringType = self.DeclaringType.MakeGenericType(arguments),
                HasThis = self.HasThis,
                ExplicitThis = self.ExplicitThis,
                CallingConvention = self.CallingConvention,
            };

            foreach (var parameter in self.Parameters)
                reference.Parameters.Add(new ParameterDefinition(parameter.ParameterType));

            foreach (var generic_parameter in self.GenericParameters)
                reference.GenericParameters.Add(new Mono.Cecil.GenericParameter(generic_parameter.Name, reference));

            return reference;
        }

        public static TypeReference FindType(this BusinessObjectBase propertyType,
            Dictionary<BusinessObjectBase, TypeReference> definedTypes)
        {
            if (propertyType == null)
                return null;
            try
            {
                return definedTypes[propertyType];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public static void ReadOnly(this PropertyDefinition p)
        {
            ModelDefault(p, "AllowEdit", "False");
        }

        public static void Size(this PropertyDefinition p, int size)
        {
            var modf = p.PropertyType.Module;
            var ctor = modf.ImportReference(typeof (SizeAttribute).GetConstructor(new[] {typeof (int)}));

            if (ctor == null)
            {
                throw new Exception("û���ҵ����ʵĹ��캯��!");
            }

            var att = new Mono.Cecil.CustomAttribute(ctor);
            att.ConstructorArguments.Add(new CustomAttributeArgument(modf.ImportReference(typeof (int)), size));
            p.CustomAttributes.Add(att);
        }

        public static void AddNonPersistentDc(this TypeDefinition type)
        {
            var modf = type.Module;
            var ctor = modf.ImportReference(typeof(NonPersistentAttribute).GetConstructor(Type.EmptyTypes));

            if (ctor == null)
            {
                throw new Exception("û���ҵ����ʵĹ��캯��!");
            }

            var att = new Mono.Cecil.CustomAttribute(ctor);
            type.CustomAttributes.Add(att);
        }

        public static void Assocication(this PropertyDefinition self, CollectionProperty p1)
        {
#warning δ�����!
            //var name = p1.DisplayName + "-" + p1.RelationProperty.DisplayName;
            //if (p1.RelationProperty is Property)
            //{
            //    var p = p1.RelationProperty as Property;
            //    if (p.BusinessObject!=null && p.BusinessObject.IsRuntimeDefine)
            //    {
            //        var pt = p.BusinessObject;
            //        if (pt != null)
            //        {
            //            var relationType = pt.GetTypeDefintion();
            //            if (relationType == null)
            //            {
            //                throw new Exception("����,û���ҵ����Ͷ���!");
            //            }
            //            var relationPropertyDefine = relationType.Properties.Single(x => x.Name == p.����);
            //            relationPropertyDefine.Assocication(name);
            //        }
            //        else
            //        {
            //            throw new Exception("��������ֻ�ܽ������û��������������.ϵͳ���õ��������޷����ĵ�!�����Ҫ�����������͵Ĺ���,���Լ̳и�����,���������Խ�������!");
            //        }
            //    }
            //    else
            //    {
            //        throw new Exception("��������ֻ�ܽ������û��������������.ϵͳ���õ��������޷����ĵ�!�����Ҫ�����������͵Ĺ���,���Լ̳и�����,���������Խ�������!");
            //    }
            //}
            //self.Assocication(name);
            

        }


        public static void Assocication(this PropertyDefinition self, string name)
        {
            var mod = self.DeclaringType.Module;

            var clrctor = typeof(AssociationAttribute).GetConstructor(new Type[] { typeof(string) });

            var ctor = self.DeclaringType.Module.ImportReference(clrctor);

            if (ctor == null)
            {
                throw new Exception("û���ҵ����ʵĹ��캯��!");
            }

            var att = new Mono.Cecil.CustomAttribute(ctor);
            att.ConstructorArguments.Add(new CustomAttributeArgument(mod.ImportReference(typeof(string)), name));
            self.CustomAttributes.Add(att);
        }


        public static void Aggregate(this PropertyDefinition self)
        {
            var mod = self.DeclaringType.Module;

            var clrctor = typeof(AggregatedAttribute).GetConstructor(Type.EmptyTypes);

            var ctor = self.DeclaringType.Module.ImportReference(clrctor);

            if (ctor == null)
            {
                throw new Exception("û���ҵ����ʵĹ��캯��!");
            }

            var att = new Mono.Cecil.CustomAttribute(ctor);
            self.CustomAttributes.Add(att);
        }

        public static void ModelDefault(this PropertyDefinition p, string name, string value)
        {
            var cb = GetModelDefaultCustomAttribute(name, value, p.PropertyType.Module);
            p.CustomAttributes.Add(cb);
        }

        public static void PersistentAlias(this PropertyBuilder p, string expression)
        {
            var ctor = typeof(PersistentAliasAttribute).GetConstructor(new[] { typeof(string) });
            var cb = new CustomAttributeBuilder(ctor, new object[] { expression });
            p.SetCustomAttribute(cb);
        }

        public static void ModelDefault(this TypeDefinition t, string name, string value)
        {
            var mod = t.Module;
            var cb = GetModelDefaultCustomAttribute(name, value,mod);
            t.CustomAttributes.Add(cb);
        }

        public static void VisibileInReport(this TypeDefinition t, bool visible)
        {
            var mod = t.Module;
            var ctor = mod.ImportReference(typeof (VisibleInReportsAttribute).GetConstructor(new[] {typeof (bool)}));
            var cb = new Mono.Cecil.CustomAttribute(ctor);
            cb.ConstructorArguments.Add(new CustomAttributeArgument(mod.ImportReference(typeof (bool)), visible));
            t.CustomAttributes.Add(cb);
        }

        private static Mono.Cecil.CustomAttribute GetModelDefaultCustomAttribute(string name, string value,
            ModuleDefinition mod)
        {
            var ctor =
                mod.ImportReference(
                    typeof (ModelDefaultAttribute).GetConstructor(new[] {typeof (string), typeof (string)}));
            var cb = new Mono.Cecil.CustomAttribute(ctor);
            var tr = mod.ImportReference(typeof (string));
            cb.ConstructorArguments.Add(new CustomAttributeArgument(tr, name));
            cb.ConstructorArguments.Add(new CustomAttributeArgument(tr, value));
            return cb;
        }

        public static void Aggregate(this PropertyBuilder p)
        {
            var ctor = typeof(AggregatedAttribute).GetConstructor(Type.EmptyTypes);
            var cb = new CustomAttributeBuilder(ctor, new object[] { });
            p.SetCustomAttribute(cb);
        }
    }
}