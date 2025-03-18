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
        style [] [
            str """
                :root {
                    --text-color: #212529;
                    --bg-color: #fff;
                    --nav-bg: #f8f9fa;
                    --border-color: #999;
                    --secondary-text: #666;
                }

                @media (prefers-color-scheme: dark) {
                    :root {
                        --text-color: #e1e1e1;
                        --bg-color: #1a1a1a;
                        --nav-bg: #2d2d2d;
                        --border-color: #666;
                        --secondary-text: #999;
                    }
                }

                body {
                    color: var(--text-color);
                    background-color: var(--bg-color);
                }

                .navbar {
                    background-color: var(--nav-bg) !important;
                }

                .navbar-light .navbar-brand {
                    color: var(--text-color);
                }

                .checklist-dots {
                    border-bottom: 1px dotted var(--border-color);
                    flex: 1;
                    margin: 0 8px;
                }

                .checklist-item {
                    display: flex;
                    align-items: baseline;
                    margin-bottom: 0.5rem;
                }

                @media (min-width: 768px) {
                    .checklist-title-secondary {
                        text-align: right;
                    }
                }

                @media (max-width: 767px) {
                    .checklist-title-secondary {
                        display: block;
                        font-size: 0.9em;
                        color: var(--secondary-text);
                    }
                }

                .alert-warning {
                    background-color: #2d2000;
                    border-color: #664d00;
                    color: #ffdd99;
                }

                @media (prefers-color-scheme: light) {
                    .alert-warning {
                        background-color: #fff3cd;
                        border-color: #ffecb5;
                        color: #664d03;
                    }
                }
            """
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

let pageLayout title content =
    html [_lang "en"] [
        headSection title
        body [] [
            navigationBar
            div [_class "container my-4"] [
                disclaimer
                main [] content
            ]
            script [
                _src "https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"
            ] []
        ]
    ]