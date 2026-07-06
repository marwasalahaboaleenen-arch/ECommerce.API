using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Common
{
    public class Result
    {
       public bool IsSuccess {  get; }
      public IReadOnlyList<Error> Errors { get; }


        public Result(bool isSuccess,IReadOnlyList<Error>error)
        {
          IsSuccess = isSuccess;
            Errors = error;
        }
        public static Result Ok()
            =>new Result(true,Array.Empty<Error>());

        public static Result Fail(Error error)
            => new Result(false, new[] { error });
        public static Result Fail(IReadOnlyList<Error>errors)
            =>new Result(false,errors);










    }

    public class Result<TValue> : Result
    {
        private readonly TValue _value;
        public TValue data => IsSuccess? _value :throw new NotImplementedException("Failed to get data");

        public Result(TValue value):base(true,Array.Empty<Error>())
        {
            _value = value;
        }
        public Result(Error error):base(false, new[] {error})
        {
            _value = default!;   
        }
        public Result(IReadOnlyList<Error>errors):base(false,errors) 
        {
            _value = default!;  
        }
        public static Result<TValue> Ok(TValue value)
           => new Result<TValue>(value);

        public static Result<TValue> Fail(Error error)
            => new Result<TValue>(error);
        public static Result <TValue>Fail(IReadOnlyList<Error> errors)
            => new Result <TValue>(errors);

















    }
}
