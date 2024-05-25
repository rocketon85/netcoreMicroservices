using NetCoreMicroservices.Commons.Structures.SecurityStructure;

namespace NetCoreMicroservices.Users.Structures.SecurityStructure
{
    public class PolicyTypesStructure: PolicyTypesBaseStructure
    {
        public override string ViewDetail => $"users.view.detail.policy";

        public override string ViewList => $"users.view.list.policy";

        public override string Edit => $"users.edit.policy";

        public override string Delete => $"users.delete.policy";

        public override string Add => $"users.add.policy";
    }
}
