open System
open System.IO
open Giraffe.ViewEngine



let processChecklist filePath =
    let checklist = Parser.parseYamlFile filePath
    let page = View.render checklist
    let outputPath = Path.Combine("html", Path.GetFileNameWithoutExtension(filePath) + ".html")
    let html = RenderView.AsString.htmlDocument page
    Directory.CreateDirectory("html") |> ignore
    File.WriteAllText (outputPath, html)

[<EntryPoint>]
let main args =
    try
        let checklistFiles = Directory.GetFiles("checklists", "*.yaml")
        for file in checklistFiles do
            processChecklist file
        printfn "Successfully generated HTML files"
        0
    with
    | ex ->
        eprintfn "Error: %s" ex.Message
        1
