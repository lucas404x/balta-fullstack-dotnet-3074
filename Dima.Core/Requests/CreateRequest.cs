using Dima.Core.Entities;

namespace Dima.Core.Requests;

public class CreateRequest<T> : BaseRequestWithEntity<T> where T : BaseEntity;