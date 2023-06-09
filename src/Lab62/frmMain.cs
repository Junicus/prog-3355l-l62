using Lab61.Data;

namespace Lab62;

public partial class frmMain : Form
{
    private readonly PuntajesDbContext _context;
    private Dictionary<string, (int, int)> _ranges;

    public frmMain()
    {
        _context = new PuntajesDbContext();
        _ranges = new Dictionary<string, (int, int)>
        {
            { "De 40 a 31", (40, 31) },
            { "De 30 a 21", (30, 21) },
            { "De 20 a 11", (20, 11) },
            { "De 10 a 0", (10, 0) },
        };

        InitializeComponent();
    }

    private void btnCargar_Click(object sender, EventArgs e)
    {
        var (max, min) = _ranges[cbCalificacionRange.SelectedValue!.ToString()!];
        var actividades = _context.Actividades.Where(p =>
            p.Descripcion == cbTarea.SelectedValue!.ToString() && (p.Puntaje >= min && p.Puntaje <= max)).ToList();
        lvData.Items.Clear();
        lvData.Items.AddRange(actividades.Select(p => new ListViewItem(new[] { p.FechaActividad.ToString("dd/MM/yyyy"), p.Estudiante, p.Descripcion, p.Puntaje.ToString() })).ToArray());
    }

    private void btnLimpiar_Click(object sender, EventArgs e)
    {
        lvData.Items.Clear();
    }

    private void frmMain_Load(object sender, EventArgs e)
    {
        var tareas = _context.Actividades.Select(p => p.Descripcion).Distinct().ToList();
        cbTarea.DataSource = tareas;

        var ranges = _ranges.Keys.ToList();
        cbCalificacionRange.DataSource = ranges;
    }
}
