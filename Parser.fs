module Parser

open System
open System.IO
open Domain

let private parseTitle (line: string) =
    let parts = line.Trim([|'"'|]).Split([|" - "|], StringSplitOptions.None)
    match parts with
    | [|title; subtitle|] -> (title, subtitle)
    | [|title|] -> (title, "")
    | _ -> ("", "")

let private parseChecklistItem (line: string) =
    let parts = line.Trim([|'-'|]).Split([|" - "|], StringSplitOptions.None)
    match parts with
    | [|task; response|] -> 
        { Task = task.Trim()
          Response = response.Trim() }
    | _ -> 
        { Task = line.Trim()
          Response = "" }

let private countLeadingSpaces (line: string) = 
    line.Length - line.TrimStart().Length

let parseYamlFile (filePath: string) : Checklist =
    let lines = File.ReadAllLines filePath
    
    let rec parseLines state lines =
        match lines with
        | [] -> state
        | line::rest when String.IsNullOrWhiteSpace(line) -> 
            parseLines state rest
        | line::rest ->
            let indent = countLeadingSpaces line
            let trimmedLine = line.Trim()
            
            match indent, trimmedLine with
            | 0, title when title.StartsWith("\"") ->
                let (mainTitle, subTitle) = parseTitle trimmedLine
                parseLines { Title = mainTitle; SubTitle = subTitle; Sections = [] } rest
                
            | 2, section when section.EndsWith(":") ->
                let sectionName = section.TrimEnd(':').Trim([|'"'|])
                let currentSection = { Name = sectionName; Items = [] }
                parseLines { state with Sections = currentSection :: state.Sections } rest
                
            | 4, item when item.StartsWith("-") ->
                match state.Sections with
                | currentSection :: otherSections ->
                    let checklistItem = parseChecklistItem item
                    let updatedSection = { currentSection with Items = checklistItem :: currentSection.Items }
                    parseLines { state with Sections = updatedSection :: otherSections } rest
                | [] -> parseLines state rest
                
            | _ -> parseLines state rest
    
    let result = parseLines { Title = ""; SubTitle = ""; Sections = [] } (lines |> Array.toList)
    { result with 
        Sections = result.Sections 
                  |> List.rev 
                  |> List.map (fun section -> { section with Items = List.rev section.Items }) }
