namespace Wings.Framework.Shared.Attributes
{
    public enum WhereCondition
    {
        GreatThen,
        LessThen,
        Equals,
        Contains

    }
    public class WhereConditionPair
    {
        public string FieldName { get; set; }
        public WhereCondition Condition { get; set; }
        public object Value { get; set; }
    }
    public class WhereAttribute : System.Attribute
    {
        public WhereCondition whereCondition { get; set; }
        public string FieldName { get; set; }
        public WhereAttribute(WhereCondition _whereCondition)
        {
            whereCondition = _whereCondition;
        }
        public WhereAttribute(WhereCondition _whereCondition, string fieldName)
        {
            whereCondition = _whereCondition;
            FieldName = fieldName;
        }
    }

}