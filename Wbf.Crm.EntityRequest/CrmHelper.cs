using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Metadata.Query;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wbf.Crm.Common
{
    public class CrmHelper
    {
        /// <summary>
        /// 创建实体
        /// </summary>
        /// <param name="svc"></param>
        public static void CreateEntity(IOrganizationService svc)
        {
            CreateEntityRequest createrequest = new CreateEntityRequest
            {
                //Define the entity--实体的定义
                Entity = new EntityMetadata
                {
                    SchemaName = "new_bank",
                    DisplayName = new Label("银行", 2052),
                    DisplayCollectionName = new Label("银行", 2052),
                    Description = new Label("An entity to store information about customer bank accounts", 2052),
                    OwnershipType = OwnershipTypes.UserOwned,
                    IsActivity = false,

                },

                // Define the primary attribute for the entity--定义实体的主字段
                PrimaryAttribute = new StringAttributeMetadata
                {
                    SchemaName = "new_name",
                    RequiredLevel = new AttributeRequiredLevelManagedProperty(AttributeRequiredLevel.None),
                    MaxLength = 100,
                    FormatName = StringFormatName.Text,
                    DisplayName = new Label("Bank Name", 2052),
                    Description = new Label("The primary attribute for the Bank entity.", 2052)
                },
                SolutionUniqueName= "test"
            };
            svc.Execute(createrequest);
            Console.WriteLine("The bank account entity has been created.");

        }

        /// <summary>
        /// 获取解决方案,没有就新增
        /// </summary>
        /// <param name="svc"></param>
        /// <param name="solutionName">解决方案唯一名称</param>
        /// <returns></returns>
        public static Entity GetSolution(IOrganizationService svc,string solutionName,bool isCreated=false)
        {
            QueryExpression querySolution = new QueryExpression("solution");
            querySolution.Criteria.AddCondition("uniquename", ConditionOperator.Equal, solutionName);
            querySolution.ColumnSet = new ColumnSet(true);
            var ec = svc.RetrieveMultiple(querySolution);
            if (ec.Entities.Count > 0)
            {
                return ec.Entities[0];
            }
            else
            {
               return isCreated?CreatedSolution(svc, solutionName):null;
            }
        }

        /// <summary>
        /// 创建解决方案
        /// </summary>
        /// <param name="svc"></param>
        private static Entity CreatedSolution(IOrganizationService svc,string solutionName)
        {
            Entity solution = new Entity("solution");
            solution["uniquename"] = solutionName;
            solution["friendlyname"] = solutionName;
            solution["publisherid"] = new EntityReference("publisher", new Guid("D21AAB71-79E7-11DD-8874-00188B01E34F"));
            solution["description"] = "This solution was created by the WorkWithSolutions sample code in the Microsoft Dynamics CRM SDK samples.";
            solution["version"] = "1.0";
            solution.Id = svc.Create(solution);
            return solution;
        }

        /// <summary>
        /// 获取解决方案的所有组件
        /// </summary>
        /// <param name="svc"></param>
        /// <param name="solution"></param>
        public static EntityCollection GetSolutionComponents(IOrganizationService svc,Entity solution)
        {
            //获取解决方案的所有组件
            QueryByAttribute componentQuery = new QueryByAttribute
            {
                EntityName = "solutioncomponent",
                ColumnSet = new ColumnSet("componenttype", "objectid", "solutioncomponentid", "solutionid"),
                Attributes = { "solutionid" },

                // In your code, this value would probably come from another query.
                Values = { solution.Id }
            };
            return svc.RetrieveMultiple(componentQuery);
        }

        /// <summary>
        /// 给实体添加字段
        /// </summary>
        /// <param name="svc"></param>
        /// <param name="entity"></param>
        private static void SetFiledToEntity(IOrganizationService svc,string entityName)
        {
            RetrieveAttributeRequest attributeRequest = new RetrieveAttributeRequest
            {
                EntityLogicalName = entityName,
                LogicalName = "new_approval_status",
                RetrieveAsIfPublished = false
            };

            // Execute the request
            RetrieveAttributeResponse attributeResponse = (RetrieveAttributeResponse)svc.Execute(attributeRequest);

            CreateAttributeRequest createBankNameAttributeRequest = new CreateAttributeRequest
            {
                EntityName = entityName,
                Attribute= new PicklistAttributeMetadata("new_approval_status")
                {
                    ParentPicklistLogicalName= "new_approval_status",
                    OptionSet =new OptionSetMetadata() { ParentOptionSetName= "new_approval_status",IsGlobal=true,
                        DisplayName = new Label("审批状态", 2052),
                        Description = new Label("审批状态", 2052),
                        Name= "new_approval_status"
                    },
                    DisplayName = new Label("审批状态", 2052),
                    Description = new Label("审批状态", 2052),
                    RequiredLevel = new AttributeRequiredLevelManagedProperty(AttributeRequiredLevel.None),
                }
                //Attribute = new StringAttributeMetadata
                //{
                //    SchemaName = "new_address",
                //    RequiredLevel = new AttributeRequiredLevelManagedProperty(AttributeRequiredLevel.None),
                //    MaxLength = 100,
                //    FormatName = StringFormatName.Text,
                //    DisplayName = new Label("地址", 2052),
                //    Description = new Label("银行地址信息.", 2052)
                //}
            };
            svc.Execute(createBankNameAttributeRequest);
        }

        /// <summary>
        /// 读取解决方案的实体，并添加字段
        /// </summary>
        /// <param name="_service"></param>
        public static void SetFiledFromSolution(IOrganizationService _service)
        {
            var solution = GetSolution(_service, "test");
            var ec = GetSolutionComponents(_service, solution);
            foreach (Entity e in ec.Entities.Where(x => x.GetAttributeValue<OptionSetValue>("componenttype").Value == 1).ToList())
            {
                var request = new RetrieveEntityRequest { MetadataId = e.GetAttributeValue<Guid>("objectid") };
                RetrieveEntityResponse respon = (RetrieveEntityResponse)_service.Execute(request);
                SetFiledToEntity(_service, respon.EntityMetadata.LogicalName);
            }
        }

        /// <summary>
        /// 获取发布者
        /// </summary>
        /// <param name="_service"></param>
        public static void GetPublisher(IOrganizationService _service)
        {
            QueryExpression query = new QueryExpression("publisher");
            query.Criteria.AddCondition("uniquename", ConditionOperator.BeginsWith, "DefaultPublisher");
            var ec = _service.RetrieveMultiple(query);
        }

        private static Dictionary<string, byte[]> _files = new Dictionary<string, byte[]>();
        /// <summary>
        /// 导出解决方案
        /// </summary>
        /// <param name="svc"></param>
        /// <param name="solutionName"></param>
        public static byte[] ExportSolution(IOrganizationService svc, string solutionName)
        {
            ExportSolutionRequest request = new ExportSolutionRequest()
            {
                SolutionName = solutionName,
                Managed = false
            };
            ExportSolutionResponse exportSolutionResponse = (ExportSolutionResponse)svc.Execute(request);

            byte[] exportXml = exportSolutionResponse.ExportSolutionFile;
            return exportXml;
            //UnzipSolution(exportXml);
            //string filename = solutionName + ".zip";
            //File.WriteAllBytes(".//" + filename, exportXml);

            //Console.WriteLine("Solution exported to {0}.", ".//" + filename);
        }

        private static void UnzipSolution(byte[] data)
        {
            MemoryStream stream1 = new MemoryStream();
            stream1.Write(data, 0, data.Length);
            foreach (ZipPackagePart part in ((ZipPackage)Package.Open(stream1, FileMode.Open)).GetParts())
            {
                using (Stream stream = part.GetStream())
                {
                    long length = stream.Length;
                    byte[] buffer = new byte[length];
                    stream.Read(buffer, 0, (int)length);
                    string key = "";
                    switch (part.Uri.ToString())
                    {
                        case "/customizations.xml":
                            key = "customizations.xml";
                            break;

                        case "/solution.xml":
                            key = "solution.xml";
                            break;

                        default:
                            if (!part.Uri.ToString().StartsWith("/Formulas"))
                            {
                                continue;
                            }
                            key = part.Uri.ToString();
                            break;
                    }
                    if (_files.ContainsKey(key))
                    {
                        _files[key] = buffer;
                    }
                    else
                    {
                        _files.Add(key, buffer);
                    }
                }
            }
        }

        /// <summary>
        /// 发布自定义项
        /// </summary>
        /// <param name="srv"></param>
        public static void PublishSolution(IOrganizationService srv)
        {
            PublishAllXmlRequest publishRequest = new PublishAllXmlRequest();
            srv.Execute(publishRequest);
        }

        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="service"></param>
        /// <param name="entityName"></param>
        /// <returns></returns>
        public static RetrieveEntityResponse GetEntityResponse(IOrganizationService service, string entityName)
        {
            RetrieveEntityRequest request = new RetrieveEntityRequest();
            request.LogicalName = entityName;
            return (RetrieveEntityResponse)service.Execute(request);
        }

        /// <summary>
        /// 获取所有实体信息
        /// </summary>
        /// <returns></returns>
        public static List<EntityMetadata> GetAllEntity(IOrganizationService service)
        {
            List<EntityMetadata> crmEntities = new List<EntityMetadata>();
            RetrieveAllEntitiesRequest retrieveAll = new RetrieveAllEntitiesRequest
            {
                EntityFilters = EntityFilters.Entity,
                RetrieveAsIfPublished = true
            };
            var response = (RetrieveAllEntitiesResponse)service.Execute(retrieveAll);
            crmEntities = response.EntityMetadata
                .Where(x => x.DisplayName.UserLocalizedLabel != null && x.MetadataId != null && x.ObjectTypeCode.HasValue)
                .ToList();
            return crmEntities;
        }

        /// <summary>
        /// 获取字段元数据
        /// </summary>
        /// <param name="service"></param>
        /// <param name="entityName"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static AttributeMetadata GetFieldMetadata(IOrganizationService service,string entityName,string fieldName)
        {
            RetrieveAttributeRequest retrieveAttribute = new RetrieveAttributeRequest();
            retrieveAttribute.EntityLogicalName = entityName;
            retrieveAttribute.LogicalName = fieldName;
            var result = (RetrieveAttributeResponse)service.Execute(retrieveAttribute);
            return result.AttributeMetadata;
        }

        /// <summary>
        /// 获取实体的一对多关系
        /// </summary>
        /// <param name="entityname">实体名</param>
        /// <param name="service">组织服务</param>
        /// <returns></returns>
        public List<OneToManyRelationshipMetadata> GetEntityRelationship(IOrganizationService service, string entityname)
        {
            List<OneToManyRelationshipMetadata> models = new List<OneToManyRelationshipMetadata>();

            MetadataFilterExpression EntityFilter = new MetadataFilterExpression(LogicalOperator.And);
            EntityFilter.Conditions.Add(new MetadataConditionExpression("LogicalName", MetadataConditionOperator.Equals, entityname));

            MetadataPropertiesExpression EntityProperties = new MetadataPropertiesExpression()
            {
                AllProperties = false
            };
            EntityProperties.PropertyNames.AddRange(new string[] { "OneToManyRelationships" });
            MetadataFilterExpression AttributeFilter = new MetadataFilterExpression(LogicalOperator.Or);
            AttributeFilter.Conditions.AddRange(new MetadataConditionExpression[] { });

            RetrieveMetadataChangesRequest request = new RetrieveMetadataChangesRequest()
            {
                Query = new EntityQueryExpression()
                {
                    Criteria = EntityFilter,
                    Properties = EntityProperties,
                    RelationshipQuery = new RelationshipQueryExpression()
                    {
                        Criteria = AttributeFilter,
                        Properties = new MetadataPropertiesExpression() { AllProperties = true }
                    },
                    LabelQuery = new LabelQueryExpression()

                }
            };

            RetrieveMetadataChangesResponse resp = (RetrieveMetadataChangesResponse)service.Execute(request);
            foreach (OneToManyRelationshipMetadata item in resp.EntityMetadata[0].OneToManyRelationships)
            {
                if (item.IsCustomRelationship.HasValue && item.IsCustomRelationship.Value)
                {
                    models.Add(item);
                }
            }

            return models;
        }

        /// <summary>
        /// 获取实体的多对一关系
        /// </summary>
        /// <param name="entityname">实体名</param>
        /// <param name="service">组织服务</param>
        /// <returns></returns>
        public List<OneToManyRelationshipMetadata> GetEntityManyToOneRelationship(IOrganizationService service,string entityname)
        {
            List<OneToManyRelationshipMetadata> models = new List<OneToManyRelationshipMetadata>();

            MetadataFilterExpression EntityFilter = new MetadataFilterExpression(LogicalOperator.And);
            EntityFilter.Conditions.Add(new MetadataConditionExpression("LogicalName", MetadataConditionOperator.Equals, entityname));

            MetadataPropertiesExpression EntityProperties = new MetadataPropertiesExpression()
            {
                AllProperties = false
            };
            EntityProperties.PropertyNames.AddRange(new string[] { "ManyToOneRelationships" });
            MetadataFilterExpression AttributeFilter = new MetadataFilterExpression(LogicalOperator.Or);
            AttributeFilter.Conditions.AddRange(new MetadataConditionExpression[] { });

            RetrieveMetadataChangesRequest request = new RetrieveMetadataChangesRequest()
            {
                Query = new EntityQueryExpression()
                {
                    Criteria = EntityFilter,
                    Properties = EntityProperties,
                    RelationshipQuery = new RelationshipQueryExpression()
                    {
                        Criteria = AttributeFilter,
                        Properties = new MetadataPropertiesExpression() { AllProperties = true }
                    },
                    LabelQuery = new LabelQueryExpression()

                }
            };

            RetrieveMetadataChangesResponse resp = (RetrieveMetadataChangesResponse)service.Execute(request);
            foreach (var item in resp.EntityMetadata[0].ManyToOneRelationships)
            {
                if (item.IsCustomRelationship.HasValue && item.IsCustomRelationship.Value)
                {
                    models.Add(item);
                }
            }

            return models;
        }

        /// <summary>
        /// 获取实体视图
        /// </summary>
        /// <param name="service"></param>
        /// <param name="objecttypecode">实体编码</param>
        /// <returns></returns>
        public static EntityCollection GetEntityView(IOrganizationService service,int objecttypecode)
        {
            //查询实体对应的视图
            QueryExpression mySavedQuery = new QueryExpression
            {
                ColumnSet = new ColumnSet("savedqueryid", "name", "querytype", "isdefault", "returnedtypecode", "isquickfindquery", "fetchxml"),
                EntityName = "savedquery",
                Criteria = new FilterExpression
                {
                    Conditions =
                        {
                            //new ConditionExpression
                            //{
                            //    AttributeName = "querytype",
                            //    Operator = ConditionOperator.Equal,
                            //    Values = {0}
                            //},
                        new ConditionExpression
                                {
                                    AttributeName = "returnedtypecode",
                                    Operator = ConditionOperator.Equal,
                                    Values = { objecttypecode }
                                }
                        }
                }
            };
            RetrieveMultipleRequest retrieveSavedQueriesRequest = new RetrieveMultipleRequest { Query = mySavedQuery };

            RetrieveMultipleResponse retrieveSavedQueriesResponse = (RetrieveMultipleResponse)service.Execute(retrieveSavedQueriesRequest);

            return retrieveSavedQueriesResponse.EntityCollection;
        }

        /// <summary>
        /// 获取 实体窗体
        /// </summary>
        /// <param name="service"></param>
        /// <param name="objecttypecode"></param>
        /// <returns></returns>
        public static EntityCollection GetEntityForm(IOrganizationService service, int objecttypecode)
        {
            QueryExpression query = new QueryExpression("systemform");
            query.Criteria.AddCondition("objecttypecode", ConditionOperator.Equal, objecttypecode);
            return service.RetrieveMultiple(query);
        }

        public static string GetTest()
        {
            return "测试合并";
        }
    }
}
