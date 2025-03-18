module Views.IndexPage

open Giraffe.ViewEngine

let renderIndex (checklists: Domain.Checklist list) =
    let links =
        checklists
        |> List.map (fun cl ->
            let href = sprintf "%s.html" cl.Slug
            div [_class "mb-3"] [
                a [_href href] [str (sprintf "%s - %s" cl.Title cl.SubTitle)]
            ]
        )
    
    SharedLayout.pageLayout "Aircraft Checklists" [
        h1 [_class "mb-4"] [str "Available Checklists"]
        div [] links
    ]
