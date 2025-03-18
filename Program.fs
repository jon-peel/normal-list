open System
open System.IO
open Giraffe.ViewEngine
open Logging

let openChcecklist filePath = 
    let checklist = Parser.parseYamlFile filePath
    checklist

let processChecklist checklist =    
    logChecklistGeneration checklist
    let page = Views.CheckListPage.render checklist
    let outputPath = Path.Combine("html", checklist.Slug + ".html")
    let html = RenderView.AsString.htmlDocument page
    Directory.CreateDirectory("html") |> ignore
    File.WriteAllText (outputPath, html)

let generateIndex checklists =
    let html = Views.IndexPage.renderIndex checklists
    let indexPath = Path.Combine("html", "index.html")
    File.WriteAllText(indexPath, RenderView.AsString.htmlDocument html)

let copyWwwRoot outputPath =
    let wwwrootPath = Path.Combine(__SOURCE_DIRECTORY__, "wwwroot")
    if Directory.Exists(wwwrootPath) then
        let rec copyDir source dest =
            Directory.CreateDirectory(dest) |> ignore
            
            for file in Directory.GetFiles(source) do
                let destFile = Path.Combine(dest, Path.GetFileName(file))
                File.Copy(file, destFile, true)
                
            for dir in Directory.GetDirectories(source) do
                let destDir = Path.Combine(dest, Path.GetFileName(dir))
                copyDir dir destDir
                
        copyDir wwwrootPath outputPath

let generateSite outputPath =
    let checklistFiles = Directory.GetFiles("checklists", "*.yaml")
    let checkLists = checklistFiles |> Seq.map openChcecklist |> Seq.toList
    generateIndex checkLists
    for file in checkLists do
        processChecklist file
    copyWwwRoot outputPath

let ensureHtmlDirectory() =
    let htmlDir = Path.Combine(Directory.GetCurrentDirectory(), "html")
    if not (Directory.Exists(htmlDir)) then
        Directory.CreateDirectory(htmlDir) |> ignore

[<EntryPoint>]
let main args =
    try
        ensureHtmlDirectory()
        generateSite "html"
        logComplete()
        0
    with
    | ex ->
        eprintfn "Error: %s" ex.Message
        1
