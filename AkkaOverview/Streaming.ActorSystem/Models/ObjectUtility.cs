using Akka.Util.Internal;
using System.Text;

namespace AkkaOverview.Streaming.ActorSystem.Models
{
    public static class ObjectUtility
    {
        public static string ToObjectString(this object obj)
        {
            StringBuilder stringBuilder = new StringBuilder();

            obj.GetType().GetProperties().ForEach(property => stringBuilder.AppendLine($"{property.Name} = {property.GetValue(obj).ToString()}"));

            return stringBuilder.ToString();
        }
    }
}