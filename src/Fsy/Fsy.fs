module Fsy

open System.IO

let interpret code =
    let memory = Array.zeroCreate 30000
    let mutable ptr = 0
    let mutable bracketsCounter = 0
    let maxLoop = 10

    let loopCode =
        [| for _ in 1..maxLoop -> [] |]

    let mutable nestCounter = 0

    let rec read code =
        match code with
        | c :: code ->
            if bracketsCounter = 0 then
                match c with
                | '>' -> ptr <- ptr + 1
                | '<' -> ptr <- ptr - 1
                | '+' -> memory.[ptr] <- memory.[ptr] + 1
                | '-' -> memory.[ptr] <- memory.[ptr] - 1
                | '.' ->
                    memory.[ptr]
                    |> char
                    |> printf "%O"
                | ',' -> memory.[ptr] <- stdin.ReadLine().[0] |> int
                | '[' -> bracketsCounter <- bracketsCounter + 1
                | _ -> ()
            else
                match c with
                | '[' ->
                    bracketsCounter <- bracketsCounter + 1
                    loopCode.[nestCounter] <- '[' :: loopCode.[nestCounter]
                | ']' ->
                    bracketsCounter <- bracketsCounter - 1
                    if bracketsCounter <> 0 then
                        loopCode.[nestCounter] <- ']' :: loopCode.[nestCounter]
                    else
                        loopCode.[nestCounter] <- List.rev
                                                      loopCode.[nestCounter]
                        nestCounter <- nestCounter + 1
                        [ for _ in 1..memory.[ptr] ->
                              read loopCode.[nestCounter - 1] ]
                        |> ignore
                        nestCounter <- nestCounter - 1
                        loopCode.[nestCounter] <- []
                | any -> loopCode.[nestCounter] <- any :: loopCode.[nestCounter]
            read code
        | [] -> ()
    code
    |> Seq.toList
    |> read

[<EntryPoint>]
let main args =
    let filePath = args.[0]
    File.ReadAllLines(filePath)
    |> String.concat ""
    |> interpret
    0
