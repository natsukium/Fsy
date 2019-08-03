module Fsy

let interpet code =
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
                | ',' -> memory.[ptr] <- stdin.ReadLine() |> int
                | '[' -> bracketsCounter <- bracketsCounter + 1
                | _ -> ptr <- ptr
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
        | _ -> ptr <- ptr
    code
    |> Seq.toList
    |> read

stdin.ReadLine() |> interpet
