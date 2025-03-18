open System
open System.IO
open Giraffe.ViewEngine


let openChcecklist filePath = 
    let checklist = Parser.parseYamlFile filePath
    checklist


let processChecklist checklist =    
    let page = Views.CheckListPage.render checklist
    let outputPath = Path.Combine("html", checklist.Slug + ".html")
    let html = RenderView.AsString.htmlDocument page
    Directory.CreateDirectory("html") |> ignore
    File.WriteAllText (outputPath, html)

let generateIndex checklists =
    let html = Views.IndexPage.renderIndex checklists
    let indexPath = Path.Combine("html", "index.html")
    File.WriteAllText(indexPath, RenderView.AsString.htmlDocument html)

[<EntryPoint>]
let main args =
    try
        let checklistFiles = Directory.GetFiles("checklists", "*.yaml")
        let checkLists = checklistFiles |> Seq.map openChcecklist |> Seq.toList
        generateIndex checkLists
        for file in checkLists do
            processChecklist file
        printfn "Successfully generated HTML files"
        0
    with
    | ex ->
        eprintfn "Error: %s" ex.Message
        1
