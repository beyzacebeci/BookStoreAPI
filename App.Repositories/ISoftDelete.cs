

namespace App.Repositories;

public interface ISoftDelete
{
    bool IsDeleted { get; set; }
}
