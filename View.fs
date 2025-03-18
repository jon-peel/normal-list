module View

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
    html [] [
        head [] [
            title [] [ str checklist.Title ]
            link [ 
                _rel "stylesheet"
                _href "https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css"
            ]
            style [] [ 
                str """
                    .dotted-line {
                        border-bottom: 1px dotted #999;
                        display: block;
                        margin: 0 80px;
                        height: 1em;
                    }
                """
            ]
        ]
        body [] [
            div [ _class "container mt-4" ] [
                renderTitle checklist.Title
                div [ _class "row d-flex flex-wrap" ] [
                    yield! checklist.Sections |> List.map renderSection
                ]
            ]
        ]
    ]
