# Type Parameter 정렬 규칙

## 들어가기

### 자문자답

- 질문1. 왜 **Type Parameter 정렬 규칙** 같은 것을 정해야 하지?
- 답변1. 
  1. C# 의 **타입 시스템** 은, 내가 원하는 것에 비하면 빈약하다.
      - 나는 F* 만큼, 하다못해 Typescript 만큼 강력한 타입 정의 시스템을 원한다.
      - 나는 F* 만큼, 하다못해 F# 만큼 강력한 타입 추론 시스템을 원한다.
  2. 빈약한 C# 의 타입 시스템을 원하는 만큼 강력하게 사용하기 위해서는, C# Type Parameter 를 정렬 규칙에 따라 나열하자.

### 타입 정의

- C# 의 타입 정의 시스템은, Typescript 나 F* 의 해당 시스템에 비하면 약하다.
  - C# 은 **dependent product type** 을 지원한다.
    - 일명, Generic 타입 시스템
  - 하지만, C# 은 **dependent sum type** 을 전혀 지원하지 않는다.
    - Typescript 는 infer 나 keyof 같은 키워드를 통해 dependent sum type 을 지원한다.
    - F* 는 유저가 타입이 `Type → Type` 인 함수를 정의할 수 있다.
    - 그 때문인지, Typescript 와 F* 의 타입 시스템은 '튜링 완전' 하다...

### 타입 추론

- C# 의 타입 추론 시스템은, Typescript · F* · F# 의 해당 시스템에 비하면 약하다.
  - 이 문서에서 말하는 타입 추론 시스템에는, '타입 생략' 기능도 포함된다.
  - F# 만 해도, [Flexible Types](https://learn.microsoft.com/en-us/dotnet/fsharp/language-reference/flexible-types) 와 [Wildcards as Type Arguments](https://learn.microsoft.com/en-us/dotnet/fsharp/language-reference/generics/#wildcards-as-type-arguments) 를 통해 강력한 타입 추론 기능을 제공한다.

#### F# 의 타입 추론 예시

```fsharp
module public Wildcard

type IMapper<'TSource, 'TResult> =
    abstract member Map: source: 'TSource -> 'TResult

let public invokeMapper source (mapper: #IMapper<_,_>) =
    source |> mapper.Map
(*
val invokeMapper:
   source: 'a ->
   mapper: 'b (requires :> IMapper<'a,'c> )
        -> 'c
*)

let public invokeMapper2 source (mapper1:#IMapper<_,_>) (mapper2:#IMapper<_,_>) =
    source |> mapper1.Map |> mapper2.Map
(*
val invokeMapper2:
   source : 'a ->
   mapper1: 'b (requires :> IMapper<'a,'c> ) ->
   mapper2: 'd (requires :> IMapper<'c,'e> )
         -> 'e
*)

let public invokeMapper3 mapperKey source (mapperMap :#IMapper<_,#IMapper<_,_>>) =
    source |> (mapperKey |> mapperMap.Map).Map
(*
val invokeMapper3:
   mapperKey: 'a ->
   source   : 'b ->
   mapperMap: 'c (requires :> IMapper<'a,'d> )
           -> 'e
*)

[<Struct>]
type AdHocMapper<'TSource, 'TResult>(mapImpl: 'TSource -> 'TResult) =
    interface IMapper<'TSource, 'TResult> with
        member _.Map (source: 'TSource): 'TResult = mapImpl source

let public createMapperMap (impl: 'a -> 'b -> 'c) =
    let func2 (a:'a) = a |> impl |> AdHocMapper<_,_> // new AdHocMapper: mapImpl: ('b -> 'c) -> AdHocMapper<'b,'c>
    func2 |> AdHocMapper<_,_> // new AdHocMapper: mapImpl: ('a -> AdHocMapper<'b,'c>) -> AdHocMapper<'a,AdHocMapper<'b,'c>>
(*
val createMapperMap:
   impl: ('a -> 'b -> 'c)
      -> AdHocMapper<'a,AdHocMapper<'b,'c>>
*)

let add2MapperMap = // AdHocMapper<int,AdHocMapper<int,int>>
    let add2 a b = a+b
    add2 |> createMapperMap

let run1 a b =
    invokeMapper3 a b add2MapperMap
(*
val invokeMapper3:
   mapperKey: 'a ->
   source   : 'b ->
   mapperMap: 'c (requires :> IMapper<'a,'d> )
           -> 'e

Generic Parameters

'a is int
'b is int
'c is AdHocMapper<int,AdHocMapper<int,int>>
'd is AdHocMapper<int,int>
'e is int
*)
```

- 생각1

이 예시에서 보듯이, `Wildcard1.run1` 의 `invokeMapper3` 에는 아무런 타입 인자를 명세하지 않았는데, \
F#의 타입 추론 시스템이 저 타입 인자를 자동 명세했다. \
C# 이었으면 저 타입 인자를 일일이 다 입력해야 했을 것이다.

- 생각2

createMapperMap 의 AdHocMapper 생성자에 타입 인자 대신 와일드카드(`_`)를 입력했는데, \
F#의 타입 추론 시스템이 각 AdHocMapper 의 타입 인자를 자동 명세했다. \
C# 이었으면 저 타입 인자를 일일이 다 입력해야 했을 것이다.

- 생각3

C# 에 저 타입 와일드카드라도 있으면 좋겠다.

(작성중)