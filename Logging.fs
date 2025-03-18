module Logging

let private printLine char = 
    printfn "%s" (String.replicate 80 char)

let private centerText (text: string) =
    let padding = (80 - text.Length) / 2
    String.replicate padding " " + text

let logChecklistGeneration (checklist: Domain.Checklist) =
    printLine "="
    printfn ""
    printfn "%s" (centerText "GENERATING CHECKLIST")
    printfn ""
    printfn "  Title    : %s" checklist.Title
    printfn "  Subtitle : %s" checklist.SubTitle
    printfn ""
    printLine "-"
    printfn ""

let logComplete () =
    printfn ""
    printLine "-"
    printfn "%s" (centerText "CHECKLIST GENERATION COMPLETE")
    printLine "="
