[<RequireQualifiedAccess>]
module Thoth.Json.Net.Extra

open Thoth.Json.Net

let empty: ExtraCoders = List.empty

let inline withCustom (encoder: Encoder<'Value>) (decoder: Decoder<'Value>) (extra: ExtraCoders): ExtraCoders =
    (((=) typeof<'Value>.FullName), ((fun _ -> Encode.boxEncoder encoder), (fun _ -> Decode.boxDecoder decoder)))::extra

let inline withInt64 (extra: ExtraCoders): ExtraCoders =
    withCustom Encode.int64 Decode.int64 extra

let inline withUInt64 (extra: ExtraCoders): ExtraCoders =
    withCustom Encode.uint64 Decode.uint64 extra

let inline withDecimal (extra: ExtraCoders): ExtraCoders =
    withCustom Encode.decimal Decode.decimal extra

let inline withBigInt (extra: ExtraCoders): ExtraCoders =
    withCustom Encode.bigint Decode.bigint extra

let inline withCustomPredicate
                (encoderGenerator: BoxedEncoder[] -> BoxedEncoder)
                (decoderGenerator: BoxedDecoder[] -> BoxedDecoder)
                (predicate: string->bool)
                (extra: ExtraCoders): ExtraCoders =
    (predicate, (encoderGenerator, decoderGenerator))::extra
