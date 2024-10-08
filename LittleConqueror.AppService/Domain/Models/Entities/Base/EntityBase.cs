namespace LittleConqueror.AppService.Domain.Models.Entities.Base;

public abstract class EntityBase<TId> : IEntity<TId>
{
    public virtual TId Id { get; set; }
    int? _requestedHashCode;

    public bool IsTransient()
    {
        return Id.Equals(default(TId));
    }
    
    public override bool Equals(object obj)
    {
        if (obj == null || !(obj is EntityBase<TId>))
            return false;

        if (ReferenceEquals(this, obj))
            return true;

        if (GetType() != obj.GetType())
            return false;

        var item = (EntityBase<TId>)obj;

        if (item.IsTransient() || IsTransient())
            return false;
        else
            return item == this;
    }

    public override int GetHashCode()
    {
        if (!IsTransient())
        {
            if (!_requestedHashCode.HasValue)
                _requestedHashCode = Id.GetHashCode() ^ 31; // XOR for random distribution (http://blogs.msdn.com/b/ericlippert/archive/2011/02/28/guidelines-and-rules-for-gethashcode.aspx)

            return _requestedHashCode.Value;
        }
        else
            return base.GetHashCode();
    }
}