using NetCoreMicroservices.Commons.Structures.SecurityStructure;

namespace NetCoreMicroservices.Cars.Structures.SecurityStructure
{
    public class PolicyTypesStructure: PolicyTypesBaseStructure
    {
        public override string ViewDetail => $"cars.view.detail.policy";

        public override string ViewList => $"cars.view.list.policy";

        public override string Edit => $"cars.edit.policy";

        public override string Delete => $"cars.delete.policy";

        public override string Add => $"cars.add.policy";
    }
}
