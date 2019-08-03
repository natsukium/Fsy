module Fsy

let interpet code =
    let memory = Array.zeroCreate 30000
    let mutable ptr = 0

    let rec read code =
        match code with
        | c :: code ->
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
            | _ -> ptr <- ptr
            read code
        | _ -> ptr <- ptr
    code
    |> Seq.toList
    |> read

stdin.ReadLine() |> interpet
