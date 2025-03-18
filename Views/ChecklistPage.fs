module Views.ChecklistPage

open Giraffe.ViewEngine

type ChecklistItem = {
    Item: string
    Response: string
}

type ChecklistSection = {
    Name: string
    Items: ChecklistItem list
}

type Checklist = {
    Title: string
    Sections: ChecklistSection list
}

let private renderChecklistItem item =
    div [_class "checklist-item"] [
        span [] [str item.Item]
        span [_class "checklist-dots"] []
        span [] [str item.Response]
    ]

let private renderSection section =
    div [_class "col-md-6 mb-4"] [
        div [_class "card"] [
            div [_class "card-header"] [
                str section.Name
            ]
            div [_class "card-body"] [
                yield! section.Items |> List.map renderChecklistItem
            ]
        ]
    ]

let renderChecklist (checklist: Checklist) =
    let titleParts = checklist.Title.Split(" - ")
    let mainTitle = titleParts.[0]
    let secondaryTitle = if titleParts.Length > 1 then titleParts.[1] else ""
    
    SharedLayout.pageLayout checklist.Title [
        div [_class "row mb-4"] [
            div [_class "col-md-6"] [
                h1 [] [str mainTitle]
            ]
            div [_class "col-md-6 checklist-title-secondary"] [
                h2 [] [str secondaryTitle]
            ]
        ]
        div [_class "row"] [
            yield! checklist.Sections |> List.map renderSection
        ]
    ]
