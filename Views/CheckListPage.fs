module Views.CheckListPage

open Giraffe.ViewEngine
open Domain

let private renderChecklistItem (item: ChecklistItem) =
    div [ _class "row mb-2" ] [
        div [ _class "col" ] [
            span [ _class "float-start" ] [ str item.Task ]
            span [ _class "float-end" ] [ str item.Response ]
            span [ _class "dotted-line" ] []
        ]
    ]

let private renderSection (section: ChecklistSection) =
    div [ _class "col-12 col-md-6 p-2" ] [
        div [ _class "card h-100" ] [
            div [ _class "card-header" ] [ str section.Name ]
            div [ _class "card-body" ] [
                yield! section.Items |> List.map renderChecklistItem
            ]
        ]
    ]

let private renderTitle (title: string) =
    let parts = title.Split([|" - "|], System.StringSplitOptions.None)
    match parts with
    | [| main; sub |] ->
        div [ _class "row mb-4" ] [
            div [ _class "col-12 col-md-6" ] [ h1 [] [ str main ] ]
            div [ _class "col-12 col-md-6 text-md-end" ] [ 
                h2 [ _class "text-muted" ] [ str sub ] 
            ]
        ]
    | _ -> h1 [ _class "mb-4" ] [ str title ]

let render (checklist: Checklist) =
     SharedLayout.pageLayout checklist.Title [
            div [ _class "container mt-4" ] [
                renderTitle checklist.Title
                div [ _class "row d-flex flex-wrap" ] [
                    yield! checklist.Sections |> List.map renderSection
                ]
            ]        
    ]
