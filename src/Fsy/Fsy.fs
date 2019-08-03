module Fsy

let interpet code =
    let memory = Array.zeroCreate 30000
    let mutable ptr = 0
    let mutable bracketsCounter = 0
    let mutable loopCode = []

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
                    loopCode <- '[' :: loopCode
                | ']' ->
                    bracketsCounter <- bracketsCounter - 1
                    if bracketsCounter <> 0 then loopCode <- ']' :: loopCode
                    else
                        loopCode <- List.rev loopCode
                        [ for _ in 1..memory.[ptr] -> read loopCode ]
                        |> ignore
                        loopCode <- []
                | any -> loopCode <- any :: loopCode
            read code
        | _ -> ptr <- ptr
    code
    |> Seq.toList
    |> read

stdin.ReadLine() |> interpet
