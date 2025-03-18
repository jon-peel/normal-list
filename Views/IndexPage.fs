module Views.IndexPage

open Giraffe.ViewEngine

let renderIndex (checklists: Domain.Checklist list) =
    html [] [
        head [] [
            title [] [str "Aircraft Normal Checklists"]
            link [_rel "stylesheet"; _href "https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/css/bootstrap.min.css"]
        ]
        body [] [
            div [_class "container mt-4"] [
                h1 [] [str "Aircraft Normal Checklists"]
                div [_class "alert alert-warning"] [
                    strong [] [str "Disclaimer: "]
                    str "These checklists are for simulation purposes only and should not be used for real-world aviation."
                ]
                div [_class "list-group mt-4"] (
                    checklists |> List.map (fun checklist ->
                        a [_href (sprintf "%s.html" checklist.Slug); _class "list-group-item list-group-item-action"] [
                            str checklist.Title
                        ]
                    )
                )
            ]
        ]
    ]
