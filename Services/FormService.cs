using FormBackend.Model;
using FormBackend.Services.Context;
namespace FormBackend.Services{
    public class FormService{
        private readonly DataContext _context;

        public FormService(DataContext context){
            _context = context;
        }

        public bool NewForm(FormModel newForm) => _context.FormInfo.Add(newForm) != null && _context.SaveChanges() != 0;

        public IEnumerable<FormModel> GetForms() => _context.FormInfo;

        public bool EditForm(FormModel form){
            FormModel model = _context.FormInfo.SingleOrDefault(OldForm => OldForm.ID == form.ID);
            if(model != null){
                _context.FormInfo.Update(model);
            }
            return _context.SaveChanges() != 0;
        }

        public bool DeleteForm(int id){
            FormModel model = _context.FormInfo.SingleOrDefault(OldForm => OldForm.ID == id);
            if(model != null){
                _context.FormInfo.Remove(model);
            }
            return _context.SaveChanges() != 0;
        }

        // public IEnumerable<FormModel> FilterByFirstName() => GetForms().OrderBy(item => item.FirstName);

        // public IEnumerable<FormModel> FilterByLastName() => GetForms().OrderBy(item => item.LastName);
    }
}