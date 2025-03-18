module Views.SharedLayout

open Giraffe.ViewEngine

let private headSection (titleText: string) =
    head [] [
        meta [_charset "utf-8"]
        meta [_name "viewport"; _content "width=device-width, initial-scale=1"]
        title [] [str titleText]
        link [
            _href "https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css"
            _rel "stylesheet"
        ]
        link [
            _href "./css/style.css"
            _rel "stylesheet"
        ]
    ]

let private navigationBar =
    nav [_class "navbar navbar-expand-lg navbar-light bg-light"] [
        div [_class "container"] [
            a [_class "navbar-brand"; _href "/"] [str "Aircraft Checklists"]
        ]
    ]

let private disclaimer =
    div [_class "alert alert-warning"] [
        strong [] [str "Warning: "]
        str "These checklists are for simulation purposes only and should not be used for real-world aviation."
    ]

let private footer =
    footer [_class "footer mt-auto py-3 bg-light"] [
        div [_class "container"] [
            div [_class "row"] [
                div [_class "col-12 text-center"] [
                    p [] [
                        str "Found an issue or have a request? Please "
                        a [_href "https://github.com/jon-peel/normal-list"] [str "report it on GitHub"]
                        str ". Pull requests are welcome!"
                    ]
                ]
            ]
        ]
    ]

let pageLayout title content =
    html [_lang "en"] [
        headSection title
        body [_class "d-flex flex-column min-vh-100"] [
            navigationBar
            div [_class "container my-4 flex-grow-1"] [
                disclaimer
                main [] content
            ]
            footer
            script [
                _src "https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"
            ] []
        ]
    ]