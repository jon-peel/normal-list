# Specification

## Application Specification

This is a small application.
It will read a series of YAML files and generate aircraft normal checklists for each YAML file.

### Index

An `index.html` should be generated at run time.
it should hold a disclaimer that says none of these checklists should be used for real world aviation.
It should have a link to each of the generated checklists.

### YAML checklists

Aircraft Normal Checklists are defined in YAML files, and stored in the `checklists` directory, e.g.

```yaml
"Airbus A320 - Normal Checklists" :

    "cockpit preperation" :
        - Gearpins and covers - Removed
        - Fuel Quantity - _KG
        - Seatbelts - On
        - ADIRS - Nav
        - Baro. Ref. - _SET (both)

    "Before Start" :
        - Parking Break - _
        T.O. Speeds & Thrust - _ (both)
```

### Layout

The checklists name should be on the top of the page/screen, with the secondary name (after the dash) being on the right hand side on a wide screen, or bellow the name on a smaller screen.

Each checklist (section) should be in a block, with the checkbox name at the top.

Each checklist item should be in the block on the left, with the responce (after the -) on the right. There should be dots "..." filling the space between the item name and the responce.

#### Columns
On a medium or large display the cards should be displayed in two columns:

```
[Section 1] [Section 4]
[Section 2] [Section 5]
[Section 3] [Section 6]
```

On a small display it should be in a sincle column:
```
[Section 1]
[Section 2]
[Section 3]
[Section 4]
[Section 5]
[Section 6]
```


## Technial Specification

- All coding is in F#.
- As much as possible, Functional Programing methods should be used.
- Html files are generated with GiraffeViewEngine.
- The project exports static HTML files when running.
- The checklists are defined in YAML, and experted as HTML files.
- Bootsrap should be used for styling the HTML files.

### Parsing YAML

because the YAML is so simple, it should be parsed "manually" without the use of any libraries.

### Libraries

- `Giraffe.ViewEngine` for generating HTML