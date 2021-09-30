using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace WebStore.Infrastucture.Conventions
{
    public class TestControllerConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            //controller.Actions.Add(new ActionModel());
        }
    }
}
