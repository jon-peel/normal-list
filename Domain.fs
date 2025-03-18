module Domain

type ChecklistItem = {
    Task: string
    Response: string
}

type ChecklistSection = {
    Name: string
    Items: ChecklistItem list
}

type Checklist = {
    Title: string
    SubTitle: string
    Sections: ChecklistSection list
}
