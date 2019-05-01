using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Umbraco.Web.WebApi;

namespace UmbracoV7.ApiControllers
{
    public class ContentServiceController : UmbracoApiController
    {
        public const string TestNodeAlias = "testNode";

        [HttpGet]
        public bool CreateTestNodes(int quantity)
        {
            var root = Umbraco.TypedContentAtRoot().First();
            CreateTestNodesUnderParent(quantity, root.Id);
            return true;
        }

        [HttpGet]
        public bool CreateNestedTestNodes(int quantityPerLevel, int numberOfLevels = 1)
        {
            var root = Umbraco.TypedContentAtRoot().First();
            CreateTestNodeForEachParentLimitByLevel(quantityPerLevel, numberOfLevels, 1, new List<int> { root.Id });
            return true;
        }

        private void CreateTestNodeForEachParentLimitByLevel(int numberOfNodes, int maxLevels, int currentLevel, IEnumerable<int> parents)
        {
            if (currentLevel > maxLevels)
            {
                return;
            }

            var nextLevel = currentLevel + 1;
            foreach (var parent in parents)
            {
                var newParents = CreateTestNodesUnderParent(numberOfNodes, parent);
                CreateTestNodeForEachParentLimitByLevel(numberOfNodes, maxLevels, nextLevel, newParents);
            }

        }

        private List<int> CreateTestNodesUnderParent(int quantity, int parentId)
        {
            var retval = new List<int>();
            for (var i = 0; i < quantity; i++)
            {
                var newItem = Services.ContentService.CreateContent(Guid.NewGuid().ToString(), parentId, TestNodeAlias);
                var publishAttempt = Services.ContentService.SaveAndPublishWithStatus(newItem);
                if (publishAttempt.Success)
                {
                    retval.Add(newItem.Id);
                }
            }
            return retval;
        }

        [HttpGet]
        public bool DeleteTestNodes(bool emptyRecyle = true)
        {
            Services.ContentService.DeleteContentOfType(Services.ContentTypeService.GetContentType(TestNodeAlias).Id);

            if (emptyRecyle)
            {
                Services.ContentService.EmptyRecycleBin();
            }

            return true;
        }
    }
}