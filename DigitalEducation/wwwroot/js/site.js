// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

async function fetchSections(){
    const response = await fetch(window.location.origin + `/api/section`);
    return  await response.json();
}

async function fetchSectionsAsOptionsArray(){
    const sections = await fetchSections()
    const options = [];
    sections.forEach(el => options.push(new Option(el.name, el.id)));
    return options;
}

async function searchSubsections(sectionId){
    const response = 
        await fetch(window.location.origin + `/api/subsection/search?sectionId=${sectionId}`);
    return await response.json();
}

async function fetchSubsectionsAsOptions(sectionId){
    const subsections = await searchSubsections(sectionId);
    const options = [];
    subsections.forEach(el => options.push(new Option(el.name, el.id)));
    return options;
}

async function replaceSubsections(selected_section, selected_subsection){
    const sectionId = Number(selected_section.value);
    let options = await fetchSubsectionsAsOptions(sectionId);
    [...selected_subsection.options].forEach((o) => selected_subsection.options.remove(o));
    options.forEach(o => selected_subsection.options.add(o));
}