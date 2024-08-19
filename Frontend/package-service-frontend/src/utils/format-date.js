export const formatDate = (isoDateString) => {
    const date = new Date(isoDateString);
    const options = {day: 'numeric', month: 'long', year: 'numeric'};
    return date.toLocaleDateString('es-Es',options);
}